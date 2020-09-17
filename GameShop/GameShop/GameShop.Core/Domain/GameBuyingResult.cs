using System;
using System.Collections.Generic;

namespace GameShop.Core
{
    public class GameBuyingResult : GameBuyingBase
    {
        public GameBuyingResultCode StatusCode { get; set; }
        public int? PurchaseId { get; set; }
    }
}