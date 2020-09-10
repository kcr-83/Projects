using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GameShop.Core
{
    public class GameBuyingRequestProcessorTest
    {
        private GameBuyingRequestProcessor _processor;
        private readonly GameBuyingRequest _request;
        private Mock<IGameBuyingRepository> _repositoryMock;
        private Mock<IGameRepository> _repositoryGameMock;
        private bool _isGameAvailable = true;

        public GameBuyingRequestProcessorTest()
        {

            _repositoryMock = new Mock<IGameBuyingRepository>();
            _repositoryGameMock = new Mock<IGameRepository>();

            // Arrange
            _request = new GameBuyingRequest()
            {
                FirstName = "Cezary",
                LastName = "Walenciuk",
                Email = "walenciukc@gmail.com",
                Date = DateTime.Now,
                GameToBuy = new Game() { Id = 7 }
            };
            _repositoryGameMock.Setup(x => x.IsGameAvailable(_request.GameToBuy)).Returns(() => { return _isGameAvailable; });
            _processor = new GameBuyingRequestProcessor(_repositoryMock.Object, _repositoryGameMock.Object);
        }
        [Fact]
        public void ShouldNotSaveBoughtGameIfGameIsNotAvailable()
        {
            _isGameAvailable = false;
            _processor.BuyGame(_request);
            _isGameAvailable = true;
            _repositoryMock.Verify(x => x.Save(It.IsAny<GameBought>()), Times.Never);
        }
        [Fact]
        public void ShouldReturnStatusTrueWhenSendedCorrectValues()
        {
            //Act
            GameBuyingResult result = _processor.BuyGame(_request);

            //Assert
            Assert.Equal(GameBuyingResultCode.Success, result.StatusCode);
        }
        [Fact]
        public void ShouldReturnBuyingGameResultWhitRequestValues()
        {
            // Arrange


            //Act
            GameBuyingResult result = _processor.BuyGame(_request);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(_request.FirstName, result.FirstName);
            Assert.Equal(_request.LastName, result.LastName);
            Assert.Equal(_request.Email, result.Email);
            Assert.Equal(_request.Date, result.Date);
        }
        [Fact]
        public void ShouldThrowExecptionIfRequestIsNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(

                 () => _processor.BuyGame(null)

            );

            Assert.Equal("request", exception.ParamName);
        }

        [Fact]
        public void ShouldSaveBoughtGame()
        {
            GameBought savedgameBought = null;

            _repositoryMock.Setup(x => x.Save(It.IsAny<GameBought>()))

                .Callback<GameBought>(game =>
                {
                    savedgameBought = game;
                }

            );

            _processor.BuyGame(_request);

            _repositoryMock.Verify(x => x.Save(It.IsAny<GameBought>()), Times.Once);

            Assert.NotNull(savedgameBought);
            Assert.Equal(_request.FirstName, savedgameBought.FirstName);
            Assert.Equal(_request.LastName, savedgameBought.LastName);
            Assert.Equal(_request.Email, savedgameBought.Email);
            Assert.Equal(_request.Date, savedgameBought.Date);
            Assert.Equal(_request.GameToBuy.Id, savedgameBought.GameId);
        }
        [Theory]
        [InlineData(GameBuyingResultCode.Success, true)]
        [InlineData(GameBuyingResultCode.GameIsNotAvailable, false)]
        public void ShouldReturnExpectedResultCode
    (GameBuyingResultCode expectedResultCode, bool IsGameAvailable)
        {
            _isGameAvailable = IsGameAvailable;

            var result = _processor.BuyGame(_request);

            Assert.Equal(expectedResultCode, result.StatusCode);
        }
        [Theory]
        [InlineData(11, true)]
        [InlineData(null, false)]
        public void ShouldReturnExpectedBoughtGameId(int? expectedPurchaseId, bool IsGameAvailable)
        {
            _isGameAvailable = IsGameAvailable;
            if (IsGameAvailable)
            {
                _repositoryMock.Setup(x => x.Save(It.IsAny<GameBought>())).Returns(11);
            }
            var result = _processor.BuyGame(_request);

            Assert.Equal(expectedPurchaseId, result.PurchaseId);
        }
    }
}
