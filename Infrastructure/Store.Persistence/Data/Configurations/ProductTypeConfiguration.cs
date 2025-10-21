using Microsoft.EntityFrameworkCore;
using Store.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Persistence.Data.Configurations {
    public class ProductTypeConfiguration : IEntityTypeConfiguration<ProductType> {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ProductType> builder) {
            builder.Property(t => t.Name).HasColumnType("varchar").HasMaxLength(256);
        }
    }
}
