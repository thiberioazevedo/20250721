namespace Quack.Application.DTOs
{
    public class GameDTO
    {
        public int TotalKills { get; set; }
        public IList<string> Players { get; set; }
        public Dictionary<string, int> Kills { get; set; }

        public GameDTO()
        {
            Players = new List<string>();
            Kills = new Dictionary<string, int>();
        }
    }
}

