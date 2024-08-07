

using DesafioBackEnd.Application.PersistenceAbstractions;
using DesafioBackEnd.Application.UseCases.Motorcyle.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace DesafioBackEnd.Application.UseCases.Motorcyle.Consumer
{
    public class Motorcycle2024CreatedEventConsumer : IConsumer<MotorcycleCreatedEvent>
    {
        private readonly ILogger<Motorcycle2024CreatedEventConsumer> _logger;
        private readonly IMotorcycle2024Repository _motorcycleRepository;

        public Motorcycle2024CreatedEventConsumer(ILogger<Motorcycle2024CreatedEventConsumer> logger,
            IMotorcycle2024Repository motorcycleRepository)
        {
            _logger = logger;
            _motorcycleRepository = motorcycleRepository;
        }

        public Task Consume(ConsumeContext<MotorcycleCreatedEvent> context)
        {
            var message = context.Message;
            _logger.LogInformation("2024 motorcycle created: {@Motorcycle}", message);

            if(message.Year == 2024)
            {
                _motorcycleRepository.CreateAsync(new Domain.Core.Entities.Motorcycle2024
                {
                    Id = Guid.NewGuid(),
                    MotorcycleId = message.MotorCycleId
                });
            }

            return Task.CompletedTask;
        }
    }
}
