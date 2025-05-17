using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models.ProductModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
    class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {


            builder.Property(p => p.Price)
                .HasColumnType("decimal(10,2)");

            builder.HasOne(p => p.ProductBrand)
                .WithMany()
                .HasForeignKey(p => p.BrandId);

            builder.HasOne(p => p.ProductType)
                .WithMany()
                .HasForeignKey(p => p.TypeId);



        }
    }
}
