
namespace Quack.Domain.Entities
{
    public class ClientConnect: BaseEntity
    {
        public int ClientId { get; }

        public ClientConnect(string eventName, string time, int clientId)
        {
            EventName = eventName;
            Time = time;
            ClientId = clientId;
        }
        public static ClientConnect CreateInstance(string eventName, string time, string clientId)
        {
            int clientId_ = 0;
            int.TryParse(clientId, out clientId_);

            return new ClientConnect
            (
                eventName,
                time,
                clientId_
            );
        }
    }
}
