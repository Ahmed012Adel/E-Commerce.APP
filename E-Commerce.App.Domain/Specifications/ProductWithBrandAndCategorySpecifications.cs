using E_Commerce.App.Domain.Entities.Product;

namespace E_Commerce.App.Domain.Specifications
{
    public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product, int>
    {
        public ProductWithBrandAndCategorySpecifications(string? sort, int? BrandId, int? CategoryId, int pageIndex, int pageSize,string? Search) 
            : base(
                  p =>
                  (string.IsNullOrEmpty(Search) || p.NormalizedName.Contains(Search))
                  &&
                  (!BrandId.HasValue || p.BrandId == BrandId.Value) &&
                        (!CategoryId.HasValue || p.CategoryId == CategoryId.Value)
                  )
        {
            AddIncludes();

            switch (sort)
                {
                    case "nameDesc":
                        AddOrderByDesc(p => p.Name);
                        break;

                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;

                    case "priceDesc":
                        AddOrderByDesc(p => p.Price);
                        break ;

                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }

            ApplyPagination(pageSize * (pageIndex-1), pageSize);
        }
        public ProductWithBrandAndCategorySpecifications(int id) : base (id)
        {
            AddIncludes();
        }

        private protected override void AddIncludes()
        {
            base.AddIncludes();
            Includes.Add(P => P.Brand!);
            Includes.Add(P => P.Category!);
        }


    }
}
