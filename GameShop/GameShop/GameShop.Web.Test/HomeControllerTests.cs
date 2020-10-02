using GameShop.Core;
using GameShop.Web.Controllers;
using GameShop.Web.Model;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GameShop.Web
{
    public class HomeControllerTests
    {
        private Mock<IGameBuyingRequestProcessor> _processorMock;
        private HomeController _homeController;
        private GameBuyModel _buyGameModel;
        private GameBuyingRequest _request;

        public HomeControllerTests()
        {
            _processorMock = new Mock<IGameBuyingRequestProcessor>();
            _homeController = new HomeController(_processorMock.Object);

            _buyGameModel = new GameBuyModel()
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

            _request = new GameBuyingRequest()
            {
                FirstName = "Cezary",
                LastName = "Walenciuk",
                Email = "Walenciuk@c.com",
                Date = DateTime.Now,
                GameToBuy = new Game() { Id = 99, Name = "Mortal Kombat" }
            };

            _processorMock.Setup(x => x.BuyGame(It.IsAny<GameBuyingRequest>()))
                .Returns(new GameBuyingResult()
                {
                    PurchaseId = 11,
                    StatusCode = GameBuyingResultCode.Success
                });
        }
        [Fact]
        public void ShouldCallGameBuyingRequestProcessor()
        {
            //Act
            _homeController.BuyGame(_buyGameModel);

            //Assert
            Assert.Equal(_buyGameModel.FirstName, _request.FirstName);
            Assert.Equal(_buyGameModel.LastName, _request.LastName);
            Assert.Equal(_buyGameModel.Email, _request.Email);
            Assert.Equal(_buyGameModel.Game.Id, _request.GameToBuy.Id);
            Assert.Equal(_buyGameModel.Game.Name, _request.GameToBuy.Name);

            _processorMock.Verify(x => x.BuyGame
            (It.IsAny<GameBuyingRequest>()), Times.Once);
        }

        [Theory]
        [InlineData(0, false)]
        [InlineData(1, true)]
        public void ShouldCallGameBuyingRequestProcessorIfModelIsValid
    (int expectedNumberOfCalls, bool isModelValid)
        {
            if (!isModelValid)
            {
                _homeController.ModelState.AddModelError("JustTest", "AnErrorMessage");
            }

            //Act
            _homeController.BuyGame(_buyGameModel);

            //Assert
            _processorMock.Verify(x => x.BuyGame
            (It.IsAny<GameBuyingRequest>()), Times.Exactly(expectedNumberOfCalls));
            _homeController.ModelState.Clear();
        }

        [Fact]
        public void ShouldAddModelErrorIfGameIsNotAvailabe()
        {
            //Arangge
            _processorMock.Setup(x => x.BuyGame(It.IsAny<GameBuyingRequest>()))
                .Returns(new GameBuyingResult()
                {
                    PurchaseId = null,
                    StatusCode = GameBuyingResultCode.GameIsNotAvailable
                });

            //Act
            var actionResult = _homeController.BuyGame(_buyGameModel) as ViewResult;
            Assert.NotNull(actionResult);

            var errorModel = actionResult.Model;
            Assert.NotNull(errorModel);

            var checktype = errorModel is ErrorModel;
            Assert.True(checktype);
        }
        [Fact]
        public void ShouldRedirectToGameIsNotAvaliableView()
        {
            _processorMock.Setup(x => x.BuyGame(It.IsAny<GameBuyingRequest>()))
        .Returns(new GameBuyingResult()
        {
            PurchaseId = null,
            StatusCode = GameBuyingResultCode.GameIsNotAvailable
        });

            IActionResult actionResult = _homeController.BuyGame(_buyGameModel);

            Assert.IsType<ViewResult>(actionResult);
            var viewResult = actionResult as ViewResult;

            Assert.NotNull(viewResult.ViewName);
            Assert.Equal(viewResult.ViewName, "Views/Home/GameIsNotAvaliable.cshtml");
        }

        [Fact]
        public void ShouldRedirectToSuccessView()
        {
            IActionResult actionResult = _homeController.BuyGame(_buyGameModel);

            Assert.IsType<ViewResult>(actionResult);
            var viewResult = actionResult as ViewResult;

            Assert.NotNull(viewResult.ViewName);
            Assert.Equal(viewResult.ViewName, "Views/Home/Success.cshtml");
        }
    }
}
