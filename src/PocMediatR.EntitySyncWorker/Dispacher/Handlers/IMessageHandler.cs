namespace PocMediatR.EntitySyncWorker.Dispacher.Handlers
{
    public interface IMessageHandler<T>
    {
        Task HandleAsync(T message, CancellationToken cancellationToken);
    }
}
