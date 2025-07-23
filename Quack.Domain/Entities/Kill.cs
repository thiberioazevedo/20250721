using System.Text.RegularExpressions;

namespace Quack.Domain.Entities
{
    public class Kill: BaseEntity
    {
        public int KillerId { get; }
        public string Killer { get; }
        public int VictimId { get; }
        public string Victim { get; }
        public int ModId { get; }
        public string Weapon { get; }
        private Kill(string eventName, string time, int killerId, string killer, int victimId, string victim, int modId, string weapon)
        {
            EventName = eventName;
            Time = time;
            KillerId = killerId;
            Killer = killer;
            VictimId = victimId;
            Victim = victim;
            ModId = modId;
            Weapon = weapon;
        }

        public static Kill CreateInstance(string eventName, string time, string content)
        {
            var partes = content.Split(':');

            if (partes.Length != 2)
                throw new ArgumentException("Kill event inválido. Esperado: killerId, victimId, modId e mensagem.");
            
            var propertyList = partes[0].Split(" ");

            if (propertyList.Length != 3)
                throw new ArgumentException("Kill event inválido. Esperado: killerId, victimId, modId e mensagem.");

            int.TryParse(propertyList[0], out var killerId);
            int.TryParse(propertyList[1], out var victimId);
            int.TryParse(propertyList[2], out var modId);

            string description = partes[1].Trim();

            string killer = "", victim = "", weapon = "";

            var match = Regex.Match(description, @"^(.*?) killed (.*?) by (.*)$");
            if (match.Success)
            {
                killer = match.Groups[1].Value.Trim();
                victim = match.Groups[2].Value.Trim();
                weapon = match.Groups[3].Value.Trim();
            }

            return new Kill(eventName, time, killerId, killer, victimId, victim, modId, weapon);
        }
    }
}
