using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Persistence.Data.Configurations {
    public class OrderConfigurations : IEntityTypeConfiguration<Order> {
        public void Configure(EntityTypeBuilder<Order> builder) {

            builder.OwnsOne(o => o.ShippingAddress);
            
            builder.HasOne(o => o.DeliveryMethod)
                .WithMany()
                .HasForeignKey(o => o.DeliveryMethodId);

            builder.HasMany(o => o.Items)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);


            builder.Property(o => o.Subtotal).HasColumnType("decimal(18,2)");

        }
    }
}
