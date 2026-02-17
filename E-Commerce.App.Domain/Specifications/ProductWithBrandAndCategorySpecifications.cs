using E_Commerce.App.Domain.Entities.Product;

namespace E_Commerce.App.Domain.Specifications
{
    public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product, int>
    {
        public ProductWithBrandAndCategorySpecifications() : base()
        {
            AddIncludes();
        }
        public ProductWithBrandAndCategorySpecifications(int id) : base (id)
        {
            AddIncludes();
        }
                
        private void AddIncludes()
        {
            Includes.Add(P => P.Brand!);
            Includes.Add(P => P.Category!);
        }
    }
}
