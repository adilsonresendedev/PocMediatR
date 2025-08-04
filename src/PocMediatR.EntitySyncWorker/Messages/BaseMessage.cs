namespace PocMediatR.EntitySyncWorker.Messages
{
    public class BaseEntityMessage
    {
        public string EntityType { get; set; } = default!;
        public string Payload { get; set; } = default!;
    }
}
