using Moq;
using Quack.Application.Services;
using Quack.Domain.Entities;
using Quack.Domain.Interfaces;

    namespace Quack.Tests.Application.Services
    {
    public class LogParserServiceTests
    {
        [Fact]
        public void ReadLines_WithValidGame_ReturnsGameReportDTO()
        {
            // Arrange
            var mockFactory = new Mock<IEntityFactory>();

            var game = Game.CreateInstance("InitGame", "0:00", new Dictionary<string, string>());
            game.AddPlayer("PlayerOne");
            game.AddKill(Kill.CreateInstance("Kill", "0:01", "4 3 1: PlayerOne killed PlayerTwo by MOD_RAILGUN"));

            mockFactory.Setup(f => f.ReadLines(It.IsAny<IList<string>>()))
                       .Returns(new List<Game> { game });

            var service = new LogParserService(mockFactory.Object);
            var lines = new List<string> { "  0:00 InitGame: \\someKey\\someValue" };

            // Act
            var result = service.ReadLines(lines);

            // Assert
            Assert.Single(result);
            var dto = result.First();

            Assert.Equal(2, dto.Kills.Count);
            Assert.Equal(1, dto.TotalKills);
            Assert.Equal(2, dto.Players.Count);
            Assert.Contains("PlayerOne", dto.Players.FirstOrDefault());
            Assert.Contains("PlayerTwo", dto.Players.LastOrDefault());
        }

        [Fact]
        public void ReadLines_WithEmptyInput_ReturnsEmptyList()
        {
            // Arrange
            var mockFactory = new Mock<IEntityFactory>();
            mockFactory.Setup(f => f.ReadLines(It.IsAny<IList<string>>()))
            .Returns(new List<Game>());

            var service = new LogParserService(mockFactory.Object);
            var lines = new List<string>();

            // Act
            var result = service.ReadLines(lines);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void ReadLines_WithMultipleGames_ReturnsMultipleDTOs()
        {
            // Arrange
            var mockFactory = new Mock<IEntityFactory>();

            var game1 = Game.CreateInstance("InitGame", "0:00", new Dictionary<string, string>());
            game1.AddPlayer("PlayerOne");
            game1.AddKill(Kill.CreateInstance("Kill", "0:01", "2 4 7: PlayerOne killed PlayerTwo by MOD_ROCKET_SPLASH"));

            var game2 = Game.CreateInstance("InitGame", "1:00", new Dictionary<string, string>());
            game2.AddPlayer("PlayerThree");

            mockFactory.Setup(f => f.ReadLines(It.IsAny<IList<string>>()))
                        .Returns(new List<Game> { game1, game2 });

            var service = new LogParserService(mockFactory.Object);
            var lines = new List<string> { "mocked content" };

            // Act
            var result = service.ReadLines(lines);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal(2, result.First().Players.Count);
            Assert.Equal("PlayerOne", result.First().Players.First());
            Assert.Equal("PlayerTwo", result.First()!.Players.Last());
            Assert.Equal("PlayerThree", result.Last().Players.Last());
            Assert.Single(result.Last().Players);
        }
    }
}
