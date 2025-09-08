using FluentValidation;

namespace PocMediatR.Application.Features.Queries.Get
{
    public class GetPriceTypeCommandValidator : AbstractValidator<GetPriceTypeCommand>
    {
        public GetPriceTypeCommandValidator()
        {
            RuleFor(t => t.Id)
                .NotNull()
                .NotEmpty();
        }
    }
}