using E_Commerce.App.Domain.Entities.Product;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.App.Infrastructure.presistent._Data.Config.ProductConfig
{
    internal class CategoryConfiguration : BaseEntityConfiguration<ProductCategory,int>
    {
        public override void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            base.Configure(builder);

            builder.Property(C => C.Name)
                .IsRequired()
                .HasMaxLength(35);
        }
    }
}
