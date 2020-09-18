using GameShop.Core;
using GameShop.Core.Interface;
using GameShop.Web.Controllers;
using GameShop.Web.Model;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GameShop.Web
{
    public class HomeControllerTests
    {
        [Fact]
        public void ShouldCallGameBuyingRequestProcessor()
        {
            //Arange
            var processorMock = new Mock<IGameBuyingRequestProcessor>();
            var controller = new HomeController(processorMock.Object);

            var buyGame = new GameBuyModel()
            {
                FirstName = "Cezary",
                Email = "Walenciuk@c.com",
                LastName = "Walenciuk",
                Game = new GameModel()
                {
                    Id = 99,
                    Name = "Mortal Kombat"
                }
            };

            var request = new GameBuyingRequest()
            {
                FirstName = "Cezary",
                LastName = "Walenciuk",
                Email = "Walenciuk@c.com",
                Date = DateTime.Now,
                GameToBuy = new Game() { Id = 99, Name = "Mortal Kombat" }
            };

            //Act
            controller.BuyGame(buyGame);

            //Assert


            Assert.Equal(buyGame.FirstName, request.FirstName);
            Assert.Equal(buyGame.LastName, request.LastName);
            Assert.Equal(buyGame.Email, request.Email);
            Assert.Equal(buyGame.Game.Id, request.GameToBuy.Id);
            Assert.Equal(buyGame.Game.Name, request.GameToBuy.Name);

            processorMock.Verify(x => x.BuyGame
            (It.IsAny<GameBuyingRequest>()), Times.Once);
        }
    }
}
