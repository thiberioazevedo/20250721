namespace Quack.Domain.Entities
{
    public class ClientBegin : BaseEntity
    {
        public int ClientId { get; }
        private ClientBegin(string eventName, string time, int clientId)
        {
            EventName = eventName;
            Time = time;
            ClientId = clientId;
        }

        public static BaseEntity CreateInstance(string eventName, string time, string clientId)
        {
            int id = 0;

            int.TryParse(clientId, out id);

            return new ClientBegin(eventName, time, id);
        }
    }
}
