using System;

namespace GameShop.Core
{
    public class GameBuyingRequestProcessor
    {
        private IGameBuyingRepository _repository;

        public GameBuyingRequestProcessor(IGameBuyingRepository repository)
        {
            this._repository = repository;
        }

        private static T Create<T>(GameBuyingRequest request) where T : GameBuyingBase, new()
        {
            return new T()
            {
                Date = request.Date,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName
            };
        }
        public GameBuyingResult BuyGame(GameBuyingRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));


            GameBought gameBought = Create<GameBought>(request);

            _repository.Save(gameBought);

            var result = Create<GameBuyingResult>(request);

            result.IsStatusOk = true;
            result.Errors = new System.Collections.Generic.List<string>();

            return result;
        }
    }
}