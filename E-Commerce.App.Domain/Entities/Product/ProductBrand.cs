using E_Commerce.App.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.App.Domain.Entities.Product
{
    public class ProductBrand : BaseEntity<int>
    {
        public required string Name { get; set; }
    }
}
