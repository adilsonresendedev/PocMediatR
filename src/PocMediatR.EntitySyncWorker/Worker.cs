using PocMediatR.EntitySyncWorker.Dispacher;
using PocMediatR.EntitySyncWorker.Messages;
using PocMediatR.Infra.MessageBus;

namespace PocMediatR.EntitySyncWorker;

public class Worker : BackgroundService
{
    private readonly IConsumerMessageBus _bus;
    private readonly MessageDispatcher _dispatcher;

    public Worker(IConsumerMessageBus bus, MessageDispatcher dispatcher)
    {
        _bus = bus;
        _dispatcher = dispatcher;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return _bus.ConsumeAsync<BaseEntityMessage>(
            queue: "entity.sync",
            message: (msg) => _dispatcher.DispatchAsync(msg, stoppingToken),
            cancellationToken: stoppingToken
        );
    }
}
