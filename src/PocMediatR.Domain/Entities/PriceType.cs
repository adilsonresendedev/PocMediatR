using PocMediatR.Common.Exceptions;
using PocMediatR.Common.Translations.Resources;

namespace PocMediatR.Domain.Entities
{
    public class PriceType : BaseEntity
    {
        public string Description { get; private set; }

        public static PriceType Create(string description)
        {
            return new PriceType(description);
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
        }

        private void Validate(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                Errors.Add(new DomainException(Messages.DomainExceptionIvalidDescription_error));

            ThrowIfHasError();
        }
    }
}
