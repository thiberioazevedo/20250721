using Quack.Domain.Entities;

namespace Quack.Tests.Domain.Entities
{
    public class GameTests
    {
        [Fact]
        public void CreateInstance_WithValidProperties_CreatesGameCorrectly()
        {
            // Arrange
            var props = new Dictionary<string, string>
            {
                { "sv_hostname", "Test Server" },
                { "g_gametype", "1" },
                { "fraglimit", "20" },
                { "timelimit", "15" },
                { "mapname", "q3dm17" },
                { "gamename", "Quake III Arena" }
            };

            // Act
            var game = Game.CreateInstance("InitGame", "0:00", props);

            // Assert
            Assert.Equal("InitGame", game.EventName);
            Assert.Equal("0:00", game.Time);
            Assert.Equal("Test Server", game.ServerName);
            Assert.Equal(1, game.GameType);
            Assert.Equal(20, game.FragLimit);
            Assert.Equal(15, game.TimeLimit);
            Assert.Equal("q3dm17", game.MapName);
            Assert.Equal("Quake III Arena", game.GameName);
        }

        [Fact]
        public void AddPlayer_WhenPlayerIsNew_AddsToPlayersList()
        {
            // Arrange
            var game = CreateEmptyGame();

            // Act
            game.AddPlayer("PlayerOne");

            // Assert
            Assert.Contains("PlayerOne", game.Players);
        }

        [Fact]
        public void AddPlayer_WhenPlayerExists_DoesNotAddDuplicate()
        {
            // Arrange
            var game = CreateEmptyGame();
            game.AddPlayer("PlayerOne");

            // Act
            game.AddPlayer("PlayerOne");

            // Assert
            Assert.Single(game.Players);
        }

        [Fact]
        public void AddKill_WithNormalKill_UpdatesKillsCorrectly()
        {
            // Arrange
            var game = CreateEmptyGame();
            var kill = Kill.CreateInstance("Kill", "0:01", "2 4 7: PlayerOne killed PlayerTwo by MOD_ROCKET");

            // Act
            game.AddKill(kill);

            // Assert
            Assert.Equal(1, game.TotalKills);
            Assert.Equal(1, game.Kills["PlayerOne"]);
            Assert.Equal(-1, game.Kills["PlayerTwo"]);
            Assert.Contains("PlayerOne", game.Players);
            Assert.Contains("PlayerTwo", game.Players);
        }

        [Fact]
        public void AddKill_WithWorldKill_DoesNotIncrementKiller()
        {
            // Arrange
            var game = CreateEmptyGame();
            var kill = Kill.CreateInstance("Kill", "0:02", "1022 2 22: <world> killed PlayerTwo by MOD_TRIGGER_HURT");

            // Act
            game.AddKill(kill);

            // Assert
            Assert.Equal(1, game.TotalKills);
            Assert.False(game.Kills.ContainsKey("<world>"));
            Assert.Equal(-1, game.Kills["PlayerTwo"]);
            Assert.Contains("PlayerTwo", game.Players);
        }

        [Fact]
        public void AddKill_WithMultipleKills_AccumulatesCounts()
        {
            // Arrange
            var game = CreateEmptyGame();
            var kill1 = Kill.CreateInstance("Kill", "0:01", "2 3 7: PlayerOne killed PlayerTwo by MOD_RAILGUN");
            var kill2 = Kill.CreateInstance("Kill", "0:02", "2 4 7: PlayerOne killed PlayerThree by MOD_RAILGUN");

            // Act
            game.AddKill(kill1);
            game.AddKill(kill2);

            // Assert
            Assert.Equal(2, game.TotalKills);
            Assert.Equal(2, game.Kills["PlayerOne"]);
            Assert.Equal(-1, game.Kills["PlayerTwo"]);
            Assert.Equal(-1, game.Kills["PlayerThree"]);
            Assert.Equal(3, game.Players.Count);
        }

        private Game CreateEmptyGame()
        {
            var props = new Dictionary<string, string>
            {
                { "sv_hostname", "Test Server" },
                { "g_gametype", "1" },
                { "fraglimit", "20" },
                { "timelimit", "15" },
                { "mapname", "q3dm17" },
                { "gamename", "Quake III Arena" }
            };

            return Game.CreateInstance("InitGame", "0:00", props);
        }
    }
}

