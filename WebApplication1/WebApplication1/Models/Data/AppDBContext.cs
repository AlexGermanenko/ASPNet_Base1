using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1
{
    public class AppDBContext : DbContext
    {
        public DbSet<UserModel> User { get; set; }
        public DbSet<RateModel> Rate { get; set; }
        //public DbSet<UserRateModel> UserRate { get; set; }
        public DbSet<Product> Product { get; set; }

        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRateModel>()
                .HasKey(t => new { t.RateId, t.UserId });

            modelBuilder.Entity<UserRateModel>()
                .HasOne(pt => pt.Rate)
                .WithMany(p => p.RatesUsers)
                .HasForeignKey(pt => pt.RateId);

            modelBuilder.Entity<UserRateModel>()
                .HasOne(pt => pt.User)
                .WithMany(t => t.RatesUsers)
                .HasForeignKey(pt => pt.UserId);
        }
    }
}
