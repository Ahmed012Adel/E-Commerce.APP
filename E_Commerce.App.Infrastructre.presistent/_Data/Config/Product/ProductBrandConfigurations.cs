using E_Commerce.App.Domain.Entities.Product;
using E_Commerce.App.Infrastructre.presistent._Data.Config.BaseConfig;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.App.Infrastructre.presistent._Data.Config.Product
{
    internal class ProductBrandConfigurations :BaseEntityConfiguration<ProductBrand,int>
    {
        public override void Configure(EntityTypeBuilder<ProductBrand> builder)
        {
            base.Configure(builder);

            builder.Property(B => B.Name)
                .IsRequired().HasMaxLength(100);
        }
    }
}
