namespace Quack.Domain.Entities
{
    public class ShutdownGame : BaseEntity
    {
        public int ClientId { get; internal set; }

        private ShutdownGame(string eventName, string time)
        {
            EventName = eventName;
            Time = time;
        }

        public static BaseEntity CreateInstance(string eventName, string time)
        {
            return new ShutdownGame(eventName, time);
        }
    }
}
