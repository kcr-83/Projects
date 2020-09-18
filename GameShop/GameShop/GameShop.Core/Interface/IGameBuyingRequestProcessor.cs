using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Core
{
    public interface IGameBuyingRequestProcessor
    {
        GameBuyingResult BuyGame(GameBuyingRequest request);
    }
}
