using MediatR;

namespace PocMediatR.Domain.Events
{
    public class EntityChangedDomainEvent<T> : INotification
    {
        public T Entity { get; }
        public string ChangeType { get; }

        public EntityChangedDomainEvent(T entity, string changeType)
        {
            Entity = entity;
            ChangeType = changeType;
        }
    }
}
