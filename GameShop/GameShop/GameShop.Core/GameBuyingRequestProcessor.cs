using System;

namespace GameShop.Core
{
    public class GameBuyingRequestProcessor
    {
        public GameBuyingRequestProcessor()
        {
        }

        public GameBuyingResult BuyGame(GameBuyingRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var result = new GameBuyingResult();
            result.FirstName = request.FirstName;
            result.LastName = request.LastName;
            result.Date = request.Date;
            result.Email= request.Email;
            result.IsStatusOk = true;
            result.Errors = new System.Collections.Generic.List<string>();
            return result;
        }
    }
}