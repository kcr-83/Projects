using GameShop.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace GameShop.DataAccess
{
    public class GameRepositoryTest
    {
        [Fact]
        public void ShouldGetAllGames()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GameContext>()
                .UseInMemoryDatabase(databaseName: "ShouldGetAllGames")
                .Options;

            var sortedList = new List<Game>
            {
                new Game() { Name = "M"},
                new Game() { Name = "N"},
                new Game() { Name = "O"}
            };
            using (var context = new GameContext(options))
            {
                foreach (var game in sortedList)
                {
                    context.Add(game);
                    context.SaveChanges();
                }
            }
            // Act
            List<Game> actualList;
            using (var context = new GameContext(options))
            {
                var repository = new GameRepository(context);
                actualList = repository.GetAll().ToList();
            }
            // Assert
            Assert.Equal(sortedList.Count(), actualList.Count);
        }
        [Fact]
        public void ShouldCallIsGameAvaliableWithFalse()
        {
            // Arrange
            var date = new DateTime(2020, 1, 25);

            var options = new DbContextOptionsBuilder<GameContext>()
              .UseInMemoryDatabase(databaseName: "ShouldCallIsGameAvaliableWithFalse")
              .Options;

            Game game1 = new Game { Id = 1, Name = "M" };
            Game game2 = new Game { Id = 2, Name = "K" };
            Game game3 = new Game { Id = 3, Name = "I" };
            using (var context = new GameContext(options))
            {
                context.Games.Add(game1);
                context.Games.Add(game2);
                context.Games.Add(game3);

                context.ShopWarehouseStatus
                    .Add(new GameShopWarehouseStatus { Id = 1, GameId = 1, Amount = 0 });
                context.ShopWarehouseStatus
                    .Add(new GameShopWarehouseStatus { Id = 2, GameId = 2, Amount = 8 });
                context.ShopWarehouseStatus
                    .Add(new GameShopWarehouseStatus { Id = 3, GameId = 3, Amount = 1 });

                context.SaveChanges();
            }
            using (var context = new GameContext(options))
            {
                var repository = new GameRepository(context);

                // Act
                var status1 = repository.IsGameAvailable(game1);
                var status2 = repository.IsGameAvailable(game2);
                var status3 = repository.IsGameAvailable(game3);

                // Assert
                Assert.Equal(false, status1);
                Assert.Equal(true, status2);
                Assert.Equal(true, status3);
            }
        }
    }
}
