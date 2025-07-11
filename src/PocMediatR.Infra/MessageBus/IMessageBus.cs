namespace PocMediatR.Infra.MessageBus
{
    public interface IMessageBus
    {
        Task PublishAsync(string queue, object message);
    }
}
