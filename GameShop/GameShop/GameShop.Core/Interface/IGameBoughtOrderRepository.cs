using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Core
{
    public interface IGameBoughtOrderRepository
    {
        int Save(GameBoughtOrder gameBought);
        IEnumerable<GameBoughtOrder> GetAll();
    }
}
