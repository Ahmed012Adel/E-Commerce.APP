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
    internal class ProductCategoryConfigurations:BaseEntityConfiguration<ProductCategory,int>
    {
        public override void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            base.Configure(builder);

            builder.Property(C => C.Name)
                .IsRequired().HasMaxLength(100);
        }
    }
}
