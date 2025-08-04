using PocMediatR.Domain.Entities;

namespace PocMediatR.EntitySyncWorker.Messages
{
    public class PriceTypeMessage : BaseEntityMessage
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
    }
}
