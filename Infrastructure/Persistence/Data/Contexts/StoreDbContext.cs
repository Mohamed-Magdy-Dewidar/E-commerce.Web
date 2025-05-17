using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models.ProductModule;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data.Contexts
{
    public class StoreDbContext : DbContext
    {



        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyReference).Assembly);
        }

    }
}
