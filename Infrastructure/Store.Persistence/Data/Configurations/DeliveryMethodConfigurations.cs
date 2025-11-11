using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Persistence.Data.Configurations {
    public class DeliveryMethodConfigurations : IEntityTypeConfiguration<DeliveryMethod> {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder) {
        
            builder.Property(d => d.ShortName).HasColumnType("varchar").HasMaxLength(100);
            builder.Property(d => d.Description).HasColumnType("varchar").HasMaxLength(256);
            builder.Property(d => d.DeliveryTime).HasColumnType("varchar").HasMaxLength(128);
            builder.Property(d => d.Price).HasColumnType("decimal(18,2)");

        }
    }
}
