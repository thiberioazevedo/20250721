namespace Quack.Domain.Entities
{
    public class Game : BaseEntity
    {
        public string ServerName { get; }
        public int GameType { get; }
        public int FragLimit { get; }
        public int TimeLimit { get; }
        public string MapName { get; }
        public string GameName { get; }
        public int TotalKills { get; private set; }
        public IList<string> Players { get; }
        public Dictionary<string, int> Kills { get; }

        private Game(string eventName, string time, string serverName, int gameType, int fragLimit, int timeLimit, string mapName, string gameName)
        {
            EventName = eventName;
            Time = time;
            ServerName = serverName;
            GameType = gameType;
            FragLimit = fragLimit;
            TimeLimit = timeLimit;
            MapName = mapName;
            GameName = gameName;

            TotalKills = 0;
            Players = new List<string>();
            Kills = new Dictionary<string, int>();
        }

        public static Game CreateInstance(string eventName, string time, Dictionary<string, string> propertyList)
        {
            Console.WriteLine($"Evento: {eventName}");

            foreach (var (key, value) in propertyList)
            {
                Console.WriteLine($"{key}: {value}");
            }

            return new Game
            (
                eventName,
                time,
                propertyList.GetValueOrDefault("sv_hostname", "Unknown Server"),
                int.TryParse(propertyList.GetValueOrDefault("g_gametype"), out var gt) ? gt : 0,
                int.TryParse(propertyList.GetValueOrDefault("fraglimit"), out var fl) ? fl : 0,
                int.TryParse(propertyList.GetValueOrDefault("timelimit"), out var tl) ? tl : 0,
                propertyList.GetValueOrDefault("mapname", "Unknown Map"),
                propertyList.GetValueOrDefault("gamename", "Unknown Game")
            );
        }

        public void AddKill(Kill kill)
        {
            TotalKills++;

            if (kill.Killer != "<world>")
            {
                if (!Kills.ContainsKey(kill.Killer))
                    Kills[kill.Killer] = 0;
                
                Kills[kill.Killer]++;

                AddPlayer(kill.Killer);
            }

            if (!Kills.ContainsKey(kill.Victim))
                Kills[kill.Victim] = 0;

            Kills[kill.Victim]--;

            AddPlayer(kill.Victim);
        }

        public void AddPlayer(string name)
        {
            if (!Players.Contains(name))
                Players.Add(name);
        }
    }
}
