using GameShop.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.DataAccess
{
    public class GameContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<GameShopWarehouseStatus> ShopWarehouseStatus { get; set; }
        public GameContext(DbContextOptions<GameContext> options) :
        base(options)
        {
        }
    }
}
