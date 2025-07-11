using MediatR;

namespace PocMediatR.Domain.Events
{
    public class EntityCreatedDomainEvent : INotification
    {
        public Guid EntityId { get; }
        public string EntityType { get; }

        public EntityCreatedDomainEvent(Guid entityId, string entityType)
        {
            EntityId = entityId;
            EntityType = entityType;
        }
    }
}
