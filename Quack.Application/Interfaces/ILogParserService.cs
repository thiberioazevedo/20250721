using Quack.Application.DTOs;

namespace Quack.Application.Interfaces
{
    public interface ILogParserService
    {
        IList<GameDTO> ReadLines(IList<string> stringList);
    }
}
