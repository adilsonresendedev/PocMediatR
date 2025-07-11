using MediatR;
using PocMediatR.Domain.Events;
using PocMediatR.Infra.MessageBus;

namespace PocMediatR.Application.Features.Events
{
    public class EntityCreatedDomainEventHandler : INotificationHandler<EntityCreatedDomainEvent>
    {
        private readonly IMessageBus _messageBus;

        public EntityCreatedDomainEventHandler(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        public async Task Handle(EntityCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var message = new
            {
                Id = notification.EntityId,
                Type = notification.EntityType,
                CreatedAt = DateTime.UtcNow
            };

            await _messageBus.PublishAsync("entity.created", message);
        }
    }
}
