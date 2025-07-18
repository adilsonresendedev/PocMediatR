using PocMediatR.Common.Exceptions;
using PocMediatR.Common.Translations.Resources;
using PocMediatR.Domain.Events;

namespace PocMediatR.Domain.Entities
{
    public class PriceType : BaseEntity
    {
        public string Description { get; private set; }

        public static PriceType Create(string description)
        {
            var priceType = new PriceType(description);

            priceType.AddDomainEvent(
                new EntityChangedDomainEvent<PriceType>(priceType, nameof(Create)));

            return priceType;
        }

        private PriceType(string description)
        {
            Validate(description);
            Description = description;
        }

        public void Update(string description)
        {
            Validate(description);
            Description = description;

            AddDomainEvent(
                new EntityChangedDomainEvent<PriceType>(this, nameof(Update)));
        }

        private void Validate(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                Errors.Add(new DomainException(Messages.DomainExceptionIvalidDescription_error));

            ThrowIfHasError(); 
        }
    }
}