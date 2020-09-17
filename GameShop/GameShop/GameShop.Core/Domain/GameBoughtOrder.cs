using System;

namespace GameShop.Core
{
    public class GameBoughtOrder : GameBuyingBase
    {
        public int Id { get; set; }
        public int GameId { get; set; }
    }
}