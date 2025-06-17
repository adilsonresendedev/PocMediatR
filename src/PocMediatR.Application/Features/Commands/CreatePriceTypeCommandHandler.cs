using FluentValidation;
using MediatR;
using PocMediatR.Domain.Context;

namespace PocMediatR.Application.Features.Commands
{
    public class CreatePriceTypeCommandHandler(IEnumerable<AbstractValidator<CreatePriceTypeCommand>> validators,
        IPocMediatRContext context) : HandlerBase<CreatePriceTypeCommand, Unit>(validators)
    {
        public override Task<Unit> ProcessHandler(CreatePriceTypeCommand request, CancellationToken cancellationToken)
        {
            return default;
        }
    }
}
