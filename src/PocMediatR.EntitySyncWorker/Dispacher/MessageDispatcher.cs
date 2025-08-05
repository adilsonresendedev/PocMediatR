using PocMediatR.Domain.Entities;
using PocMediatR.EntitySyncWorker.Dispacher.Handlers;
using PocMediatR.EntitySyncWorker.Messages;
using System.Text.Json;

namespace PocMediatR.EntitySyncWorker.Dispacher
{   
    public class MessageDispatcher : IMessageDispacher
    {
        private readonly IServiceProvider _provider;

        public MessageDispatcher(IServiceProvider provider)
        {
            _provider = provider;
        }

        public async Task DispatchAsync(BaseEntityMessage envelope, CancellationToken cancellationToken)
        {
            using var scope = _provider.CreateScope();

            switch (envelope?.EntityType)
            {
                case nameof(PriceType):
                    var priceType = JsonSerializer.Deserialize<PriceTypeMessage>(envelope.Payload);
                    var handler = scope.ServiceProvider.GetRequiredService<IMessageHandler<PriceTypeMessage>>();
                    await handler.HandleAsync(priceType!, cancellationToken);
                    break;

                default:
                    throw new NotSupportedException($"Entity type '{envelope?.EntityType}' is not supported.");
            }
        }
    }
}
