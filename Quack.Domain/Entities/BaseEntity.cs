namespace Quack.Domain.Entities
{
    public abstract class BaseEntity
    {
        public string? EventName { get; internal set; }

        public string? Time { get; internal set; }
    }
}
