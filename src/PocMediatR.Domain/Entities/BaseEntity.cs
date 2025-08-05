using MediatR;
using PocMediatR.Common.Exceptions;
using System.Text.Json.Serialization;

namespace PocMediatR.Domain.Entities
{
    public abstract class BaseEntity
    {
        [JsonIgnore]
        private List<INotification>? _domainEvents;
        public IReadOnlyCollection<INotification>? DomainEvents => _domainEvents?.AsReadOnly();
        public Guid Id { get; protected set; }
        [JsonIgnore]
        protected List<DomainException> Errors { get; set; } = new();

        protected BaseEntity()
        {
            Id = Guid.NewGuid();
        }

        protected BaseEntity(Guid id)
        {
            Id = id == Guid.Empty ? Guid.NewGuid() : id;
        }

        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents ??= new List<INotification>();
            _domainEvents.Add(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        protected void ThrowIfHasError()
        {
            if (Errors.Any())
            {
                throw new AggregateException(Errors);
            }
        }
    }
}
