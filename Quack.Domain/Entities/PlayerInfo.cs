using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quack.Domain.Entities
{
    public class PlayerInfo
    {
        public string Name { get; internal set; }
        public string Model { get; internal set; }
        public string HModel { get; internal set; }
        public string RedTeam { get; internal set; }
        public string BlueTeam { get; internal set; }
        public string Color1 { get; internal set; }
        public string Color2 { get; internal set; }
        public int Health { get; internal set; }
        public int Weapon { get; internal set; }
        public int Location { get; internal set; }
        public int TeamTask { get; internal set; }
        public int TeamLeader { get; internal set; }

        public static PlayerInfo CreateInstance(string eventName, Dictionary<string, string> propertyList)
        {
            throw new NotImplementedException();
        }
    }
}
