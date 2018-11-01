using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BurgerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BurgerApi.Data
{
    public class BurgerContext : DbContext
    {
        public BurgerContext(DbContextOptions<BurgerContext> options) : base(options)
        {
            
        }

        /// <summary>
        /// When the database is created, EF creates tables that have names the same as the DbSet
        /// property names. Property names for collections are typically plural.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Burger>().ToTable("Burger");
            modelBuilder.Entity<BurgerBase>().ToTable("BurgerBase");
            modelBuilder.Entity<BurgerImage>().ToTable("BurgerImage");
            modelBuilder.Entity<Cuisine>().ToTable("Cuisine");
        }


        public DbSet<Burger> Burgers { get; set; }
        public DbSet<BurgerBase> BurgerBases { get; set; }
        public DbSet<BurgerImage> Images { get; set; }
        public DbSet<Cuisine> Cuisines { get; set; }
    }
}
