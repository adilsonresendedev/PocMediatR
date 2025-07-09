using FluentValidation;
using PocMediatR.Domain.Context;
using PocMediatR.Domain.Entities;

namespace PocMediatR.Application.Features.Commands
{
    public class CreatePriceTypeCommandHandler(IEnumerable<AbstractValidator<CreatePriceTypeCommand>> validators,
        IPocMediatRContext context) : HandlerBase<CreatePriceTypeCommand, CreatePriceTypeCommandResponse>(validators)
    {
        public override async Task<CreatePriceTypeCommandResponse> ProcessHandler(CreatePriceTypeCommand request, CancellationToken cancellationToken)
        {
            var priceType = PriceType.Create(request.Description);

            await context.PriceTypes.AddAsync(priceType, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);

            return new CreatePriceTypeCommandResponse
            {
                Id = priceType.Id,
                Description = priceType.Description
            };
        }
    }
}
 