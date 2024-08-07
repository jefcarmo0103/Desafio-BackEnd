using DesafioBackEnd.Application.PersistenceAbstractions;
using DesafioBackEnd.Domain.Core.Entities;
using DesafioBackEnd.Domain.Core.Models;
using MediatR;

namespace DesafioBackEnd.Application.UseCases.Moto.Commands
{
    public sealed record UpdateMotorcycleCommand(Guid Id, string Plate) : IRequest<OperationResult<Guid>> { }

    public class UpdateMotorcycleCommandHandler : IRequestHandler<UpdateMotorcycleCommand, OperationResult<Guid>>
    {
        private readonly IMotorcycleRepository _repository;
        public UpdateMotorcycleCommandHandler(IMotorcycleRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult<Guid>> Handle(UpdateMotorcycleCommand request, CancellationToken cancellationToken)
        {
            OperationResult<Motorcycle> searched = await _repository.GetByIdAsync(request.Id);
            if (searched is { Data: null })
                return OperationResult<Guid>.Fail("Moto não encontrada");

            if(searched.Data.Plate == request.Plate.Trim())
                return OperationResult<Guid>.Fail("A placa que você está alterando é a mesma já registrada");
            else
            {
                OperationResult<Motorcycle> samePlate = await _repository.GetByPlateAsync(request.Plate);
                if (samePlate is { Data: not null })
                    return OperationResult<Guid>.Fail("Já existe uma moto com essa placa");
            }

            OperationResult<Motorcycle> modelToUpdate = Motorcycle.Build(request.Id, request.Plate, searched.Data.Year, searched.Data.Model);
            if (modelToUpdate is { Data: not null })
            {
                var result = await _repository.UpdateAsync(modelToUpdate.Data);
                if (result.Data != Guid.Empty)
                    return OperationResult<Guid>.Ok(result.Data, result.Messages);

                return OperationResult<Guid>.Fail(result.Messages);
            }
            return OperationResult<Guid>.Fail(modelToUpdate.Messages);
        }
    }
}
