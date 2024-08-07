using DesafioBackEnd.Application.PersistenceAbstractions;
using DesafioBackEnd.Domain.Core.Entities;
using DesafioBackEnd.Domain.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioBackEnd.Application.UseCases.Locacao.Commands
{
    public sealed record CreateRentCommand(Guid EntregadorId, Guid PlanoLocacaoId, Guid MotoId)
        : IRequest<OperationResult<Guid>>
    { }

    public class CreateRentCommandHandler : IRequestHandler<CreateRentCommand, OperationResult<Guid>>
    {
        private readonly IRentRepository _repository;
        private readonly IRentPlanRepository _rentPlanRepository;
        private readonly IDeliveryManRepository _deliveryManRepository;
        private readonly IMotorcycleRepository _motorcycleRepository;
        public CreateRentCommandHandler(IRentRepository repository, IRentPlanRepository rentPlanRepository, 
            IDeliveryManRepository deliveryManRepository, IMotorcycleRepository motorcycleRepository)
        {
            _repository = repository;
            _rentPlanRepository = rentPlanRepository;
            _deliveryManRepository = deliveryManRepository;
            _motorcycleRepository = motorcycleRepository;
        }

        public async Task<OperationResult<Guid>> Handle(CreateRentCommand request, CancellationToken cancellationToken)
        {
            var rentPlanResult = await _rentPlanRepository.GetByIdAsync(request.PlanoLocacaoId);
            if (rentPlanResult is { Data: null })
                return OperationResult<Guid>.Fail("Plano de locação não encontrado");

            var deliveryManResult = await _deliveryManRepository.GetByIdAsync(request.EntregadorId);
            if (deliveryManResult is { Data: null })
                return OperationResult<Guid>.Fail("Entregador não encontrado");

            var motorcycleResult = await _motorcycleRepository.GetByIdAsync(request.MotoId);
            if (motorcycleResult is { Data: null })
                return OperationResult<Guid>.Fail("Moto não encontrada");

            RentPlan rentPlan = rentPlanResult.Data;
            OperationResult<Rent> modelToCreate = Rent.Build(rentPlan.QtyDays, request.EntregadorId, rentPlan.Id, request.MotoId);

            if (modelToCreate is { Data: not null })
            {
                var result = await _repository.CreateAsync(modelToCreate.Data);
                if (result.Data != Guid.Empty)
                    return OperationResult<Guid>.Ok(result.Data, result.Messages);

                return OperationResult<Guid>.Fail(result.Messages);
            }
            else
                return OperationResult<Guid>.Fail(modelToCreate.Messages);
        }
    }
}
