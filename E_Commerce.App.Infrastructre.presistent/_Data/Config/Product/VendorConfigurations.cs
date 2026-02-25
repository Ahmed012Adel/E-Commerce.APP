using E_Commerce.App.Domain.Entities.Product;
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
    internal class VendorConfigurations :BaseEntityConfiguration<Vendor,int>
    {
        public override void Configure(EntityTypeBuilder<Vendor> builder)
        {
            base.Configure(builder);

            builder.Property(B => B.Name)
                .IsRequired().HasMaxLength(100);

            builder.Property(B => B.Phone)
                .IsRequired().HasMaxLength(11).HasColumnType("varchar(11)");

            builder.Property(B => B.Password)
                .IsRequired().HasMaxLength(16);

            builder.Property(B => B.Address)
                .IsRequired();

            builder.HasMany(v => v.Products)
                .WithOne(P => P.vendor)
                .HasForeignKey(P => P.VendorId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
