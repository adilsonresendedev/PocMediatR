using FluentValidation;
using PocMediatR.Domain.Context;

namespace PocMediatR.Application.Features.Commands.CreatePriceType
{
    public class CreatePriceTypeCommandHandler(IEnumerable<AbstractValidator<CreatePriceTypeCommand>> validators,
        IPocMediatRContext context) : HandlerBase<CreatePriceTypeCommand, CreatePriceTypeCommandResponse>(validators)
    {
        public override async Task<CreatePriceTypeCommandResponse> ProcessHandler(CreatePriceTypeCommand request, CancellationToken cancellationToken)
        {
            var priceType = Domain.Entities.PriceType.Create(request.Description);

            await context.PriceTypes.AddAsync(priceType, cancellationToken);

            var affectedRows = await context.SaveChangesAsync(cancellationToken);

            if (affectedRows == 0)
                throw new InvalidOperationException();

            return new CreatePriceTypeCommandResponse
            {
                Id = priceType.Id,
                Description = priceType.Description
            };
        }
    }
}
 