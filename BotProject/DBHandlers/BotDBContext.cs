using CouponsGetBot.MetaInfo;
using CouponsGetBot.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouponsGetBot.DBHandlers
{
    public class BotDBContext : DbContext
    {
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Facility> Facilities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Constants.ConnectionString);
        }


    }

}
