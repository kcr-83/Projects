using GameShop.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Xunit;

namespace GameShop.DataAccess
{
    public class GameBuyingRepositoryTest
    {
        [Fact]
        public void ShouldSaveTheGameBoughtOrder()
        {
            // Arrange
            var options = new DbContextOptionsBuilder
                <GameBoughtOrderContext>()
              .UseInMemoryDatabase(databaseName: "ShouldSaveTheGameBoughtOrder")
              .Options;

            var gameOrder = new GameBoughtOrder
            {
                FirstName = "Cezary",
                LastName = "Walenciuk",
                Date = new DateTime(2020, 6, 25),
                Email = "walenciukC@gmail.com",
                GameId = 1
            };

            // Act
            using (var context = new GameBoughtOrderContext(options))
            {
                var repository = new GameBoughtOrderRepository(context);
                repository.Save(gameOrder);
            }

            // Assert
            using (var context = new GameBoughtOrderContext(options))
            {
                var orders = context.Orders.ToList();

                Assert.Equal(1, orders.Count);
                var storedGameOrder = orders.Single();

                Assert.Equal(gameOrder.FirstName, storedGameOrder.FirstName);
                Assert.Equal(gameOrder.LastName, storedGameOrder.LastName);
                Assert.Equal(gameOrder.Email, storedGameOrder.Email);
                Assert.Equal(gameOrder.GameId, storedGameOrder.GameId);
                Assert.Equal(gameOrder.Date, storedGameOrder.Date);
            }
        }

        [Fact]
        public void ShouldGetAllGameBoughtOrdersByDate()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<GameBoughtOrderContext>()
              .UseInMemoryDatabase(databaseName: "ShouldGetAllGameBoughtOrdersByDate")
              .Options;

            var storedList = new List<GameBoughtOrder>
                {
                    CreateGameOrder(1,new DateTime(2020, 6, 27)),
                    CreateGameOrder(2,new DateTime(2020, 6, 25)),
                    CreateGameOrder(3,new DateTime(2020, 6, 29))
                };

            var expectedList = storedList.OrderBy(x => x.Date).ToList();

            using (var context = new GameBoughtOrderContext(options))
            {
                foreach (var order in storedList)
                {
                    context.Add(order);
                    context.SaveChanges();
                }
            }
            // Act
            List<GameBoughtOrder> actualList;
            using (var context = new GameBoughtOrderContext(options))
            {
                var repository = new GameBoughtOrderRepository(context);
                actualList = repository.GetAll().ToList();
            }

            //Assert
            //CollectionAssert for NUnit
            var test = expectedList.SequenceEqual(actualList,
                new GameBoughtEqualityComparer());
            Assert.True(test);
        }
        private class GameBoughtEqualityComparer : IEqualityComparer<GameBoughtOrder>
        {
            public bool Equals([AllowNull] GameBoughtOrder x,
                [AllowNull] GameBoughtOrder y)
            {

                if (x == null || y == null)
                    return false;

                return x.Id == y.Id;
            }

            public int GetHashCode([DisallowNull] GameBoughtOrder obj)
            {
                return obj.Id.GetHashCode();
            }
        }
        private GameBoughtOrder CreateGameOrder(int id, DateTime dateTime)
        {
            return new GameBoughtOrder
            {
                Id = id,
                FirstName = "Cezary",
                LastName = "Walenciuk",
                Date = dateTime,
                Email = "walenciuk@walenciuk.com",
                GameId = 1
            };
        }
    }
}
