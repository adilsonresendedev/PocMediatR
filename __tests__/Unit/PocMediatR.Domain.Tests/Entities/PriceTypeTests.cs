using PocMediatR.Common.Exceptions;
using PocMediatR.Common.Translations.Resources;
using PocMediatR.Common.Utils;
using PocMediatR.Domain.Entities;
using Shouldly;

namespace PocMediatR.Domain.Tests.Entities
{
    public class PriceTypeTests
    {
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
    }
}
