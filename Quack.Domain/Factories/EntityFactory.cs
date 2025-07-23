using Quack.Domain.Entities;
using Quack.Domain.Interfaces;

namespace Quack.Domain.Factories
{
    public class EntityFactory: IEntityFactory
    {
        public IList<Game> ReadLines(IList<string> lineList) {
            Game? currentGame = null;

            IList<BaseEntity> entityList = new List<BaseEntity>();

            foreach (var line in lineList) {
                currentGame = ReadLine(line, currentGame, entityList);


            }

            return entityList.Where(i => i.GetType() == typeof(Game))
                             .Select(i => ((Game)i))
                             .ToList();
        }
        private Game? ReadLine(string line, Game? currentGame, IList<BaseEntity> entityList)
        {
            if (line.Length < 7)
                return currentGame;

            var position = line.IndexOf(':', 8);

            if (position <= 0)
                return currentGame;

            var eventName = line.Substring(7, position -7).Trim();
            var content = line.Substring(position + 1).Trim();
            var time = line.Substring(0, 7).Trim();

            switch (eventName)
            {
                case "InitGame":
                    currentGame = Game.CreateInstance(eventName, time, PropertyDictionary(content));
                    entityList.Add(currentGame);
                    break;

                case "ClientUserinfoChanged":
                    var splitIndex = content.IndexOf(' ');
                    string firstPart;
                    string secondPart;

                    if (splitIndex > 0)
                    {
                        firstPart = content.Substring(0, splitIndex);
                        secondPart = content.Substring(splitIndex + 1).Trim();
                    }
                    else
                    {
                        firstPart = content;
                        secondPart = string.Empty;
                    }

                    var clientUserinfoChanged = ClientUserInfoChanged.CreateInstance(eventName, time, firstPart, PropertyDictionary(secondPart));
                    entityList.Add(clientUserinfoChanged);
                    currentGame?.AddPlayer(clientUserinfoChanged.PlayerName);
                    break;

                case "Exit":
                    var exit =  Exit.CreateInstance(eventName, time, content);
                    entityList.Add(exit);
                    break;

                case "ClientConnect":
                    var clientConnect = ClientConnect.CreateInstance(eventName, time, content);
                    entityList.Add(clientConnect);
                    break;

                case "ClientBegin":
                    var clientBegin = ClientBegin.CreateInstance(eventName, time, content);
                    entityList.Add(clientBegin);
                    break;

                case "ShutdownGame":
                    var shutdownGame = ShutdownGame.CreateInstance(eventName, time);
                    entityList.Add(shutdownGame);
                    break;

                case "Item":
                    var item = Item.CreateInstance(eventName, time, content.Split(" ").ToList());
                    entityList.Add(item);
                    break;

                case "Kill":
                    var kill = Kill.CreateInstance(eventName, time, content);
                    entityList.Add(kill);
                    currentGame?.AddKill(kill);
                    break;

                default:
                    break;
            }

            return currentGame;
        }
        private Dictionary<string, string> PropertyDictionary(string data)
        {
            if (string.IsNullOrWhiteSpace(data))
                throw new ArgumentException("Linha inválida. Esperado formato '\\chave\\valor...'");

            var propertyList = new Dictionary<string, string>();
            var tokens = data.Split('\\', StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < tokens.Length - 1; i += 2)
            {
                string key = tokens[i];
                string value = tokens[i + 1];
                propertyList[key] = value;
            }

            return propertyList;
        }
    }
}
