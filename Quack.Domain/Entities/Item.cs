namespace Quack.Domain.Entities
{
    public class Item : BaseEntity
    {
        public int ClientId { get; internal set; }
        public string ItemName { get; internal set; }

        private Item(string eventName, int clientId, string itemName, string time)
        {
            EventName = eventName;
            ClientId = clientId;
            ItemName = itemName;
            Time = time;
        }

        public static BaseEntity CreateInstance(string eventName, string time, IList<string> propertyList)
        {
            if (propertyList.Count < 2)
                throw new ArgumentException("Linha inválida para o evento Item. Esperado: ClientId e ItemName.");

            int.TryParse(propertyList[0], out var clientId);
            string itemName = propertyList[1];

            return new Item(eventName, clientId, itemName, time);
        }
    }
}

