using GameShop.Core;
using System.Collections.Generic;
using System.Linq;

namespace GameShop.DataAccess
{
    public class GameBoughtOrderRepository : IGameBoughtOrderRepository
    {
        private GameBoughtOrderContext _context;

        public GameBoughtOrderRepository(GameBoughtOrderContext context)
        {
            this._context = context;
        }

        public IEnumerable<GameBoughtOrder> GetAll()
        {
            return
            _context.Orders.OrderBy(k => k.Date);
        }

        public int Save(GameBoughtOrder gameBought)
        {
            _context.Orders.Add(gameBought);
            _context.SaveChanges();
            return gameBought.Id;
        }
    }
}