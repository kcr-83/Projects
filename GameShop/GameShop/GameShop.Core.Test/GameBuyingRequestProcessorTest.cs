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

        public GameBuyingRequestProcessorTest()
        {
            
            _repositoryMock = new Mock<IGameBuyingRepository>();
            _processor = new GameBuyingRequestProcessor(_repositoryMock.Object);
            // Arrange
            _request = new GameBuyingRequest()
            {
                FirstName = "Cezary",
                LastName = "Walenciuk",
                Email = "walenciukc@gmail.com",
                Date = DateTime.Now
            };
        }

        [Fact]
        public void ShouldReturnStatusTrueWhenSendedCorrectValues()
        {
            //Act
            GameBuyingResult result = _processor.BuyGame(_request);

            //Assert
            Assert.Equal(true, result.IsStatusOk);
            Assert.Equal(0, result.Errors.Count);
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
        }
    }
}
