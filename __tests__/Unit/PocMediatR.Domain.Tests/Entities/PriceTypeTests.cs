using PocMediatR.Common.Translations.Resources;
using PocMediatR.Common.Utils;
using PocMediatR.Domain.Entities;
using Shouldly;

namespace PocMediatR.Domain.Tests.Entities
{
    public class PriceTypeTests
    {
        private const string defaultDescription = "description";

        [Fact]
        public void PriceType_Create_ShouldThrowIfAnyError()
        {
            var exception = Should.Throw<AggregateException>(() =>
            {
                PriceType.Create(string.Empty);
            });

            exception.InnerExceptions.ShouldContain(e =>
                e.Message == Translation.GetTranslatedMessage(Messages.DomainException_detail));
        }

        [Fact]
        public void PriceType_Create_ShouldCreateWithValidData()
        {
            
            var priceType = PriceType.Create(defaultDescription);

            priceType.Description.ShouldNotBeNullOrWhiteSpace();
            priceType.Id.ToString().ShouldNotBeNullOrWhiteSpace();
        }

        [Fact]
        public void PriceType_Update_ShouldThrowIfAnyError()
        {
            var priceType = PriceType.Create(defaultDescription);

            var exception = Should.Throw<AggregateException>(() =>
            {
                priceType.Update(string.Empty);
            });

            exception.InnerExceptions.ShouldContain(e =>
                e.Message == Translation.GetTranslatedMessage(Messages.DomainException_detail));
        }
    }
}
