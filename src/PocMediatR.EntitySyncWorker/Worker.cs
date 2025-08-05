using PocMediatR.EntitySyncWorker.Dispacher;
using PocMediatR.EntitySyncWorker.Messages;
using PocMediatR.Infra.MessageBus;

namespace PocMediatR.EntitySyncWorker;

public class Worker : BackgroundService
{
    private readonly IConsumerMessageBus _bus;
    private readonly IMessageDispacher _dispatcher;

    public Worker(IConsumerMessageBus bus, IMessageDispacher dispatcher)
    {
        _bus = bus;
        _dispatcher = dispatcher;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return _bus.ConsumeAsync<BaseEntityMessage>(
            queue: "entity.changed",
            message: (msg) => _dispatcher.DispatchAsync(msg, stoppingToken),
            cancellationToken: stoppingToken
        );
    }
}
