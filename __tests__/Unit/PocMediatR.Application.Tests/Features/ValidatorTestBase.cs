using System.Diagnostics.CodeAnalysis;

namespace PocMediatR.Application.Tests.Features
{
    [ExcludeFromCodeCoverage]
    public abstract class ValidatorTestBase<TRequest, TValidator> where TValidator : AbstractValidator<TRequest>
    {
        private readonly TValidator validator;
        protected ValidatorTestBase()
        {
            validator = (TValidator)Activator.CreateInstance(typeof(TValidator), Array.Empty<object>())!;
        }

        public abstract void Should_validate_request(TRequest request, bool isValid);

        protected void Validate(TRequest request, bool isValid)
        {
            var result = validator.Validate(request);

            result.IsValid.ShouldBe(isValid);

            if (!isValid)
                result.Errors.ShouldNotBeEmpty();
        }
    }
}
