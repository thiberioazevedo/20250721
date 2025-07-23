using Quack.Domain.Entities;

namespace Quack.Domain.Interfaces
{
    public interface IEntityFactory
    {
        public IList<Game> ReadLines(IList<string> line);
    }
}
