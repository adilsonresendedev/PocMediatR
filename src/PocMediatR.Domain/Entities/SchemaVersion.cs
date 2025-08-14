namespace PocMediatR.Domain.Entities
{
    public class SchemaVersion : BaseEntity
    {
        public string ScriptName { get; set; } = null!;
        public DateTime AppliedOn { get; set; } = DateTime.UtcNow;
    }
}
