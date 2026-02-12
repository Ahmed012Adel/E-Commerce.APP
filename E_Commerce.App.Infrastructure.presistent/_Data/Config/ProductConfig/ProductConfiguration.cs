using E_Commerce.App.Domain.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.App.Infrastructure.presistent._Data.Config.ProductConfig
{
    internal class ProductConfiguration : BaseEntityConfiguration<Product , int>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            base.Configure(builder);

            builder.Property(P => P.Name)
                .IsRequired()
                .HasMaxLength(35);

            builder.Property(P => P.Description).IsRequired();

            builder.Property(p=>p.Price)
                .IsRequired()
                .HasColumnType("decimal(9,4)");

            builder.HasOne(P => P.Brand)
                .WithMany()
                .HasForeignKey(p => p.BrandId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
