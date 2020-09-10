using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Core
{
    public interface IGameBuyingRepository
    {
        int Save(GameBought gameBought);
    }
}
