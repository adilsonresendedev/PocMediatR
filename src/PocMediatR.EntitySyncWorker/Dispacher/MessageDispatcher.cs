using PocMediatR.Domain.Entities;
using PocMediatR.EntitySyncWorker.Dispacher.Handlers;
using PocMediatR.EntitySyncWorker.Messages;
using System.Text.Json;

namespace PocMediatR.EntitySyncWorker.Dispacher
{
    public class MessageDispatcher
    {
        private readonly IServiceProvider _provider;

        public MessageDispatcher(IServiceProvider provider)
        {
            _provider = provider;
        }

        public async Task DispatchAsync(BaseEntityMessage envelope, CancellationToken ct)
        {
            switch (envelope.EntityType.ToLowerInvariant())
            {
                case nameof(PriceType):
                    var priceType = JsonSerializer.Deserialize<PriceTypeMessage>(envelope.Payload);
                    var handler = _provider.GetRequiredService<IMessageHandler<PriceTypeMessage>>();
                    await handler.HandleAsync(priceType!, ct);
                    break;

                default:
                    throw new NotSupportedException($"Entity type '{envelope.EntityType}' is not supported.");
            }
        }
    }
}
