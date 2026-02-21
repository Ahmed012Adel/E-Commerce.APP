using E_Commerce.App.Infrastructre.presistent._Data.Config.BaseConfig;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.App.Infrastructre.presistent._Data.Config.Product
{
    internal class ProductConfigurations : BaseEntityConfiguration<Domain.Entities.Product.Product,int>
    {
        public override void Configure(EntityTypeBuilder<Domain.Entities.Product.Product> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(p => p.NormalizedName)
               .HasMaxLength(100)
               .IsRequired();
            builder.Property(p => p.Description) 
                .IsRequired();  
            builder.Property(p => p.Price)
                .HasColumnType("decimal(9,4)")
                .IsRequired();

            builder.HasOne(P=>P.Brand)
                .WithMany()
                .HasForeignKey(P=>P.BrandId)
                .OnDelete(DeleteBehavior.SetNull);

                builder.HasOne(P => P.Category)
                .WithMany()
                .HasForeignKey(P => P.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
