namespace PocMediatR.Infra.MessageBus
{
    public interface IConsumerMessageBus
    {
        Task ConsumeAsync<T>(string queue, Func<T, Task> message, CancellationToken cancellationToken);
    }
}
