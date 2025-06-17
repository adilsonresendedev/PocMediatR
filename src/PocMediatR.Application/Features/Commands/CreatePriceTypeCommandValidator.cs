using FluentValidation;

namespace PocMediatR.Application.Features.Commands
{
    public class CreatePriceTypeCommandValidator : AbstractValidator<CreatePriceTypeCommand>
    {
        public CreatePriceTypeCommandValidator()
        {
            RuleFor(t => t.Description)
                .NotNull()
                .NotEmpty();
        }
    }
}
