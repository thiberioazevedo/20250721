namespace Quack.Domain.Entities
{
    public class Exit : BaseEntity
    {
        public string Reason { get; }

        private Exit(string eventName, string time, string reason)
        {
            EventName = eventName;
            Reason = reason;
            Time = time;
        }

        public static Exit CreateInstance(string eventName, string time, string reason)
        {
            Console.WriteLine($"Evento: {eventName}");

            return new Exit(eventName, time, reason);
        }
    }
}
