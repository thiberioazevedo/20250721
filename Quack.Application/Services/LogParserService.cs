using Quack.Application.DTOs;
using Quack.Application.Interfaces;
using Quack.Domain.Interfaces;

namespace Quack.Application.Services
{
    public class LogParserService: ILogParserService
    {
        private readonly IEntityFactory entityFactory;

        public LogParserService(IEntityFactory entityFactory)
        {
            this.entityFactory = entityFactory;
        }

        public IList<GameDTO> ReadLines(IList<string> stringList) {
            return entityFactory.ReadLines(stringList).Select(i => new GameDTO { Kills = i.Kills, Players = i.Players, TotalKills = i.TotalKills }).ToList(); //TODO: Usar AutoMapper
        }
    }
}
