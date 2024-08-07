using DesafioBackEnd.Application.Abstractions;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace DesafioBackEnd.Infra.Core.MessageBroker
{
    public class EventBus : IEventBus
    {
        private readonly ILogger<EventBus> _logger;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IBus _bus;

        public EventBus(ILogger<EventBus> logger, IPublishEndpoint publishEndpoint, IBus bus)
        {
            _logger = logger;
            _publishEndpoint = publishEndpoint;
            _bus = bus;

        }

        public async Task PublishAsync<T>(T message, string queue, CancellationToken cancellationToken = default)
        {
            string host = Environment.GetEnvironmentVariable("RABBIT_HOST") ?? throw new ArgumentNullException("Rabbit Host");

            Uri uri = new Uri($"{host}/{queue}");
            var endpoint = await _bus.GetSendEndpoint(uri);
            await endpoint.Send(message);
        }
            
    }
}
