using Microsoft.EntityFrameworkCore;
using PocMediatR.Domain.Context;
using PocMediatR.Domain.Entities;
using PocMediatR.EntitySyncWorker.Messages;

namespace PocMediatR.EntitySyncWorker.Dispacher.Handlers
{
    public class PriceTypeHandler : IMessageHandler<PriceTypeMessage>
    {
        private readonly IPocMediatrReadContext _context;

        public PriceTypeHandler(IPocMediatrReadContext context)
        {
            _context = context;
        }

        public async Task HandleAsync(PriceTypeMessage message, CancellationToken cancellationToken)
        {
            var existingPriceType = await _context
                .PriceTypes
                .FirstOrDefaultAsync(pt => pt.Id == message.Id, cancellationToken);

            if (existingPriceType == default)
            {
                await Insert(message, cancellationToken);
                return;
            }

            await Update(message, existingPriceType, cancellationToken);
        }

        private async Task Insert(PriceTypeMessage priceTypeMessage, CancellationToken cancellationToken)
        {
            var priceType = PriceType.FromSync(priceTypeMessage.Id, priceTypeMessage.Description);
            await _context.PriceTypes.AddAsync(priceType, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        private async Task Update(PriceTypeMessage priceTypeMessage, PriceType existing, CancellationToken cancellationToken)
        {
            existing.Update(priceTypeMessage.Description);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
