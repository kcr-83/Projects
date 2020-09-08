using System;
using System.Collections.Generic;

namespace GameShop.Core
{
    public class GameBuyingResult : GameBuyingBase
    {
        public bool IsStatusOk { get; set; }
        public List<string> Errors { get; set; }
    }
}