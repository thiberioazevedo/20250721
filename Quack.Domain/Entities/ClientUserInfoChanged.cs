namespace Quack.Domain.Entities
{
    public class ClientUserInfoChanged : BaseEntity
    {
        public string ClientId { get;}
        public string PlayerName { get; }
        public string Model { get; }
        public string HitModel { get; }
        public string RedTeam { get; }
        public string BlueTeam { get; }
        public string Color1 { get; }
        public string Color2 { get; }
        public int Health { get; }
        public int Weapon { get; }
        public int Location { get; }
        public int TimeTaken { get; }
        public int TimeLeft { get; }

        private ClientUserInfoChanged(
            string clientId,
            string playerName,
            string model,
            string hitModel,
            string redTeam,
            string blueTeam,
            string color1,
            string color2,
            int health,
            int weapon,
            int location,
            int timeTaken,
            int timeLeft)
        {
            ClientId = clientId;
            PlayerName = playerName;
            Model = model;
            HitModel = hitModel;
            RedTeam = redTeam;
            BlueTeam = blueTeam;
            Color1 = color1;
            Color2 = color2;
            Health = health;
            Weapon = weapon;
            Location = location;
            TimeTaken = timeTaken;
            TimeLeft = timeLeft;
        }

        public static ClientUserInfoChanged CreateInstance(string eventName, string time, string clientId, Dictionary<string, string> propertyList)
        {
            Console.WriteLine($"Evento: {eventName}");

            foreach (var (key, value) in propertyList)
            {
                Console.WriteLine($"{key}: {value}");
            }

            return new ClientUserInfoChanged(
                clientId,
                propertyList.GetValueOrDefault("n", "Unknown Player"),
                propertyList.GetValueOrDefault("model", "default"),
                propertyList.GetValueOrDefault("hmodel", "default"),
                propertyList.GetValueOrDefault("g_redteam", ""),
                propertyList.GetValueOrDefault("g_blueteam", ""),
                propertyList.GetValueOrDefault("c1", "0"),
                propertyList.GetValueOrDefault("c2", "0"),
                int.TryParse(propertyList.GetValueOrDefault("hc"), out var hc) ? hc : 0,
                int.TryParse(propertyList.GetValueOrDefault("w"), out var w) ? w : 0,
                int.TryParse(propertyList.GetValueOrDefault("l"), out var l) ? l : 0,
                int.TryParse(propertyList.GetValueOrDefault("tt"), out var tt) ? tt : 0,
                int.TryParse(propertyList.GetValueOrDefault("tl"), out var tl) ? tl : 0
            );
        }
    }
}
