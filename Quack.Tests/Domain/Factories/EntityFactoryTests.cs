using Quack.Domain.Factories;
using System.Reflection;

namespace Quack.Tests.Domain.Factories
{
    public class EntityFactoryTests
    {
        [Fact]
        public void ReadLines_WithOnlyInitGame_ReturnsSingleGame()
        {
            // Arrange
            var entityFactory = new EntityFactory();
            var lines = new List<string> { "  0:00 InitGame: \\sv_floodProtect\\1\\g_maxGameClients\\0\\...\\g_gametype\\0" };

            // Act
            var result = entityFactory.ReadLines(lines);

            // Assert
            Assert.Single(result);
            var game = result.FirstOrDefault();
            Assert.Equal("InitGame", game?.EventName);
            Assert.Equal("0:00", game?.Time);
        }

        [Fact]
        public void ReadLines_WithClientUserInfoChanged_AddsPlayerToGame()
        {
            // Arrange
            var entityFactory = new EntityFactory();
            var lines = new List<string>{
            @"  0:00 InitGame: \sv_floodProtect\1\g_maxGameClients\0",
            @"  0:25 ClientUserinfoChanged: 2 n\PlayerOne\t\0\model\sarge"};

            // Act
            var result = entityFactory.ReadLines(lines);

            // Assert
            var game = Assert.Single(result);
            Assert.Contains("PlayerOne", game.Players);
        }

        [Fact]
        public void ReadLines_WithKillEvent_AddsKillToGame()
        {
            // Arrange
            var entityFactory = new EntityFactory();
            var lines = new List<string>
            {
                "  0:00 InitGame: \\sv_floodProtect\\1\\g_maxGameClients\\0",
                "  0:30 Kill: 2 3 10: PlayerTwo killed PlayerThree by MOD_RAILGUN"
            };

            // Act
            var result = entityFactory.ReadLines(lines);

            // Assert
            var game = Assert.Single(result);
            Assert.Equal(2, game.Kills.Count);
            Assert.Equal("PlayerTwo", game.Kills.FirstOrDefault().Key);
            Assert.Equal("PlayerThree", game.Kills.LastOrDefault().Key);
        }

        [Fact]
        public void ReadLines_WithEmptyLines_ReturnsEmptyList()
        {
            // Arrange
            var entityFactory = new EntityFactory();
            var lines = new List<string>();

            // Act
            var result = entityFactory.ReadLines(lines);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void PropertyDictionary_WithMalformedString_ThrowsException()
        {
            // Arrange
            var entityFactory = new EntityFactory();

            var method = typeof(EntityFactory).GetMethod("PropertyDictionary", BindingFlags.NonPublic | BindingFlags.Instance);

            // Act & Assert
            var ex = Assert.Throws<TargetInvocationException>(() =>
                method?.Invoke(entityFactory, new object[] { "" })
            );

            Assert.IsType<ArgumentException>(ex.InnerException);
        }
    }
}