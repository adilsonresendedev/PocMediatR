using MediatR;
using PocMediatR.Domain.Entities;
using PocMediatR.Domain.Events;
using PocMediatR.Infra.MessageBus;

namespace PocMediatR.Application.Events.PriceTypeChanged
{
    public class PriceTypeChangedEventHandler : INotificationHandler<EntityChangedDomainEvent<PriceType>>
    {
        private readonly IMessageBus _messageBus;

        public PriceTypeChangedEventHandler(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        public async Task Handle(EntityChangedDomainEvent<PriceType> notification, CancellationToken cancellationToken)
        {
            var queueName = $"entity.pricetype.changed";
            var payload = new
            {
                notification.Entity,
                notification.ChangeType,
                EventDate = DateTime.UtcNow
            };

            await _messageBus.PublishAsync(queueName, payload);
        }
    }
}
