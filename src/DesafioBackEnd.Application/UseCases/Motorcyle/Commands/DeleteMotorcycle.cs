using DesafioBackEnd.Application.PersistenceAbstractions;
using DesafioBackEnd.Domain.Core.Entities;
using DesafioBackEnd.Domain.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioBackEnd.Application.UseCases.Moto.Commands
{
    public sealed record DeleteMotorcyleCommand(Guid Id) : IRequest<OperationResult<Guid>> { }

    public class DeleteMotorcyleCommandHandler : IRequestHandler<DeleteMotorcyleCommand, OperationResult<Guid>>
    {
        private readonly IMotorcycleRepository _repository;
        private readonly IMotorcycle2024Repository _motorcycle2024Repository;
        private readonly IRentRepository _rentRepository;
        public DeleteMotorcyleCommandHandler(IMotorcycleRepository repository, IRentRepository rentRepository, IMotorcycle2024Repository motorcycle2024Repository)
        {
            _repository = repository;
            _rentRepository = rentRepository;
            _motorcycle2024Repository = motorcycle2024Repository;
        }

        public async Task<OperationResult<Guid>> Handle(DeleteMotorcyleCommand request, CancellationToken cancellationToken)
        {
            OperationResult<Motorcycle> searched = await _repository.GetByIdAsync(request.Id);
            if (searched is { Data: null })
                return OperationResult<Guid>.Fail("Moto não encontrada");

            var rents = await _rentRepository.GetOpenRentsByMotorcycleIdAsync(request.Id);
            if(rents is { Data: not null })
                return OperationResult<Guid>.Fail("Não é possível remover esse moto porquê essa está vinculada há uma ou mais locações");

            OperationResult<Motorcycle> modelToRemove = Motorcycle.Build(request.Id, searched.Data.Plate, searched.Data.Year, searched.Data.Model);
            if (modelToRemove is { Data: not null })
            {
                if(modelToRemove.Data.Year == 2024)
                    await RemoveIfAre2024(request.Id);

                var result = await _repository.DeleteAsync(modelToRemove.Data);
                if (result.messages.ToList().Exists(x => x.sucess))
                    return OperationResult<Guid>.Ok(request.Id, "Operação executada com sucesso");

                return OperationResult<Guid>.Fail(result.messages);
            }
            else
                return OperationResult<Guid>.Fail(modelToRemove.Messages);
        }

        private async Task RemoveIfAre2024(Guid id)
        {
            var moto2024 = await _motorcycle2024Repository.GetByMotorcycleIdAsync(id);
            if (moto2024 is { Data: not null })
                await _motorcycle2024Repository.DeleteAsync(moto2024.Data);
        }
    }
}
