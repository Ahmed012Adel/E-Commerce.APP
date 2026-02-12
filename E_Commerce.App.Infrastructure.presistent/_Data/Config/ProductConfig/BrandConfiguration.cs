using E_Commerce.App.Domain.Entities.Product;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.App.Infrastructure.presistent._Data.Config.ProductConfig
{
    internal class BrandConfiguration :BaseEntityConfiguration<ProductBrand , int>
    {
        public override void Configure(EntityTypeBuilder<ProductBrand> builder)
        {
            base.Configure(builder);

            builder.Property(B => B.Name)
                .IsRequired()
                .HasMaxLength(35);
        }
    }
}
