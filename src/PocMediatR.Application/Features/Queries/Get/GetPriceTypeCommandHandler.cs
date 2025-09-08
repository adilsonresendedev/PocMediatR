using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PocMediatR.Application.Exceptions;
using PocMediatR.Domain.Context;
using PocMediatR.Domain.Entities;

namespace PocMediatR.Application.Features.Queries.Get
{
    public class GetPriceTypeCommandHandler(IEnumerable<AbstractValidator<GetPriceTypeCommand>> validators,
        IPocMediatrWriteContext context) : HandlerBase<GetPriceTypeCommand, GetPriceTypeCommandResponse>(validators)
    {
        public override async Task<GetPriceTypeCommandResponse> ProcessHandler(GetPriceTypeCommand request, CancellationToken cancellationToken)
        {
            var priceType = await context
                .PriceTypes
                .FirstOrDefaultAsync(pt => pt.Id == request.Id);

            if (priceType == default)
                throw new ResourceNotFoundException<PriceType>();

            return new GetPriceTypeCommandResponse
            {
                Id = priceType.Id,
                Descritption = priceType.Description
            };
        }
    }
}