using System;

namespace GameShop.Core
{
    public class GameBuyingRequestProcessor : IGameBuyingRequestProcessor
    {
        private IGameBoughtOrderRepository _repository;
        private IGameRepository _gameRepository;

        public GameBuyingRequestProcessor(IGameBoughtOrderRepository repository,
    IGameRepository gameRepository)
        {
            _repository = repository;
            _gameRepository = gameRepository;
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


            GameBoughtOrder gameBought = Create<GameBoughtOrder>(request);
            gameBought.GameId = request.GameToBuy.Id;
            var result = Create<GameBuyingResult>(request);
            if (_gameRepository.IsGameAvailable(request.GameToBuy)){
                
                result.PurchaseId = _repository.Save(gameBought);
                result.StatusCode = GameBuyingResultCode.Success;
            }
            else
            {
                result.StatusCode = GameBuyingResultCode.GameIsNotAvailable;
            }      

            

            return result;
        }
    }
}