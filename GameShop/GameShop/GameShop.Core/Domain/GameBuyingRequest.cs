using System;

namespace GameShop.Core
{
    public class GameBuyingRequest : GameBuyingBase
    {
        public GameBuyingRequest()
        {
        }

        public Game GameToBuy { get; set; }
    }

}