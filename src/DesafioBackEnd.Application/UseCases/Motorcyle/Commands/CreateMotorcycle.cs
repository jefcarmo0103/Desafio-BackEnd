using DesafioBackEnd.Application.Abstractions;
using DesafioBackEnd.Application.PersistenceAbstractions;
using DesafioBackEnd.Application.UseCases.Motorcyle.Events;
using DesafioBackEnd.Domain.Core.Entities;
using DesafioBackEnd.Domain.Core.Models;
using MediatR;

namespace DesafioBackEnd.Application.UseCases.Moto.Commands
{
    public sealed record CreateMotorcycleCommand(int Year, string Model, string Plate) : IRequest<OperationResult<Guid>> { }

    public class CreateMotorcycleCommandHandler : IRequestHandler<CreateMotorcycleCommand, OperationResult<Guid>>
    {
        private readonly IMotorcycleRepository _repository;
        private readonly IEventBus _eventBus;

        public CreateMotorcycleCommandHandler(IMotorcycleRepository motorcycleRepository, IEventBus eventBus)
        {
            _repository = motorcycleRepository;
            _eventBus = eventBus;
        }

        public async Task<OperationResult<Guid>> Handle(CreateMotorcycleCommand request, CancellationToken cancellationToken)
        {
            OperationResult<Motorcycle> searched = await _repository.GetByPlateAsync(request.Plate);
            if (searched is { Data: not null })
                return OperationResult<Guid>.Fail("Já existe uma moto com essa placa");

            OperationResult<Motorcycle> modelToCreate = Motorcycle.Build(request.Plate, request.Year, request.Model);
            if (modelToCreate is { Data: not null })
            {
                var result = await _repository.CreateAsync(modelToCreate.Data);
                if (result.Data != Guid.Empty)
                {
                    var message = new MotorcycleCreatedEvent(result.Data, request.Year);
                    await _eventBus.PublishAsync(message, message.QUEUE_DESTINATION, cancellationToken);
                    return OperationResult<Guid>.Ok(result.Data, result.Messages);
                }
                    
                
                return OperationResult<Guid>.Fail(result.Messages);
            }
            return OperationResult<Guid>.Fail(modelToCreate.Messages);
        }
    }
}
