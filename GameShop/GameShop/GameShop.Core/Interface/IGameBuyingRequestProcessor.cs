using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.Core.Interface
{
    public interface IGameBuyingRequestProcessor
    {
        GameBuyingResult BuyGame(GameBuyingRequest request);
    }
}
