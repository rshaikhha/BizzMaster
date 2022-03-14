using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace API.Data
{
    public class BMContext : IdentityDbContext<User>
    {
        public BMContext(DbContextOptions options) : base(options)
        {
        }

        #region Basics
        public DbSet<Country> Countries { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<UsageType> UsageTypes { get; set; }
        public DbSet<MasterSystem> MasterSystems { get; set; }

        #endregion


        #region Car

        public DbSet<Platform> Platforms { get; set; }
        public DbSet<Car> Cars { get; set; }

        #endregion


        #region Product
        public DbSet<Product> Products {get; set;}

        #endregion

        #region Supplier
        public DbSet<Supplier> Suppliers {get; set;}
        public DbSet<SupplyLine> SupplyLines {get; set;}


        #endregion

        public DbSet<SalesForecast> SalesForecasts {get; set;}
        public DbSet<Stock> Stocks {get; set;}
        public DbSet<Order> Orders {get; set;}
        public DbSet<Shipment> Shipments {get; set;}
        public DbSet<LeadTime> LeadTimes {get; set;}
        public DbSet<CommercialCard> CommercialCards {get; set;}
        public DbSet<OrderRegistration> OrderRegistrations {get; set;}
        public DbSet<Project> Projects {get; set;}


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>()
                .HasData(
                    new IdentityRole { Name = "Member", NormalizedName = "MEMBER" },
                    new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" }
                );
        }
    }
}