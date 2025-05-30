using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Configurations
{

    internal class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");


            builder.Property(O => O.SubTotal)
                .HasColumnType("decimal(8,2)");

            builder.HasMany(O => O.Items).
                WithOne()
                .OnDelete(DeleteBehavior.Cascade);


            #region PaymentStatus
            builder.Property(order => order.OrderStatus)
                .HasConversion(S => S.ToString(),
                S => Enum.Parse<OrderStatus>(S))
                .HasMaxLength(150);


            #endregion

            builder.HasOne(O => O.DeliveryMethod).
                   WithMany()
                  .HasForeignKey(O => O.DeliveryMethodId);


            builder.OwnsOne<OrderAddress>(O => O.OrderAddress);


        }
    }
}
