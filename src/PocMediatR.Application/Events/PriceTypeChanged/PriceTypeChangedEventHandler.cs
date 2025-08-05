using MediatR;
using PocMediatR.Domain.Entities;
using PocMediatR.Domain.Events;
using PocMediatR.Infra.MessageBus;
using System.Text.Json;

namespace PocMediatR.Application.Events.PriceTypeChanged
{
    public class PriceTypeChangedEventHandler : INotificationHandler<EntityChangedDomainEvent<PriceType>>
    {
        private readonly IPublisherMessageBus _messageBus;

        public PriceTypeChangedEventHandler(IPublisherMessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        public async Task Handle(EntityChangedDomainEvent<PriceType> notification, CancellationToken cancellationToken)
        {
            var queueName = $"entity.changed";
            var payload = new
            {
                Payload = JsonSerializer.Serialize(notification.Entity),
                EntityType = nameof(PriceType),
                EventDate = DateTime.UtcNow
            };

            await _messageBus.PublishAsync(queueName, payload, cancellationToken);
        }
    }
}
