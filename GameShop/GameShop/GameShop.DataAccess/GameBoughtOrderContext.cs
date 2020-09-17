using GameShop.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.DataAccess
{
    public class GameBoughtOrderContext : DbContext
    {
        public DbSet<GameBoughtOrder> Orders { get; set; }
        public GameBoughtOrderContext(DbContextOptions<GameBoughtOrderContext> options) : base(options)
        {

        }
    }
}
