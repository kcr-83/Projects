using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GameShop.Core
{
    public class GameBuyingRequestProcessorTest
    {
        private GameBuyingRequestProcessor _processor;

        public GameBuyingRequestProcessorTest()
        {
            _processor = new GameBuyingRequestProcessor();
        }

        [Fact]
        public void ShouldReturnStatusTrueWhenSendedCorrectValues()
        {
            // Arrange
            var request = new GameBuyingRequest()
            {
                FirstName = "Cezary",
                LastName = "Walenciuk",
                Email = "walenciukc@gmail.com",
                Date = DateTime.Now
            };

            //Act
            GameBuyingResult result = _processor.BuyGame(request);

            //Assert
            Assert.Equal(true, result.IsStatusOk);
            Assert.Equal(0, result.Errors.Count);
        }
        [Fact]
        public void ShouldReturnBuyingGameResultWhitRequestValues()
        {
            // Arrange
            var request = new GameBuyingRequest()
            {
                FirstName = "Cezary",
                LastName = "Walenciuk",
                Email = "walenciukc@gmail.com",
                Date = DateTime.Now
            };

            //Act
            var processor = new GameBuyingRequestProcessor();
            GameBuyingResult result = processor.BuyGame(request);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(request.FirstName, result.FirstName);
            Assert.Equal(request.LastName, result.LastName);
            Assert.Equal(request.Email, result.Email);
            Assert.Equal(request.Date, result.Date);
        }
        [Fact]
        public void ShouldThrowExecptionIfRequestIsNull()
        {
            var processor = new GameBuyingRequestProcessor();


            var exception = Assert.Throws<ArgumentNullException>(

                 () => processor.BuyGame(null)

            );

            Assert.Equal("request", exception.ParamName);
        }
    }
}
