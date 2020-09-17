using GameShop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameShop.DataAccess
{
    public class GameRepository : IGameRepository
    {
        private GameContext _context;
        public GameRepository(GameContext context)
        {
            _context = context;
        }
        public IEnumerable<Game> GetAll()
        {
            return _context.Games.ToList();
        }

        public bool IsGameAvailable(Game game)
        {
            var shopWarehouseStatus = _context.ShopWarehouseStatus.
        FirstOrDefault(x => x.GameId == game.Id);

            if (shopWarehouseStatus == null)
                return false;

            return shopWarehouseStatus.Amount > 0;
        }
    }
}
