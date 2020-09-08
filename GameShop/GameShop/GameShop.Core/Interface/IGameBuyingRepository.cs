using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Core
{
    public interface IGameBuyingRepository
    {
        void Save(GameBought gameBought);
    }
}
