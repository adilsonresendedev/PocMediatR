using PocMediatR.EntitySyncWorker.Messages;

namespace PocMediatR.EntitySyncWorker.Dispacher
{
    public interface IMessageDispacher
    {
        Task DispatchAsync(BaseEntityMessage envelope, CancellationToken cancellationToken);
    }
}
