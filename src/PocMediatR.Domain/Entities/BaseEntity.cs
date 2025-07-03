using PocMediatR.Common.Exceptions;

namespace PocMediatR.Domain.Entities
{
    public abstract class BaseEntity
    {
        protected List<DomainException> Errors { get; set; } = new();

        protected void ThrowIfHasError()
        {
            if (Errors.Any())
            {
                throw new AggregateException(Errors);
            }
        }

        public Guid Id { get; protected set; }

        protected BaseEntity()
        {
            Id = Guid.NewGuid();
        }

        protected BaseEntity(Guid id)
        {
            Id = id == Guid.Empty ? Guid.NewGuid() : id;
        }
    }
}
