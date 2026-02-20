using E_Commerce.App.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.App.Domain.Specifications
{
    public class ProductWithFilterationForCountPagination : BaseSpecifications<Product , int>
    {
        public ProductWithFilterationForCountPagination( int? BrandId, int? CategoryId)
            : base(p => (!BrandId.HasValue || p.BrandId == BrandId.Value) &&
                        (!CategoryId.HasValue || p.CategoryId == CategoryId.Value)
                  )
        {
            
        }
    }
}
