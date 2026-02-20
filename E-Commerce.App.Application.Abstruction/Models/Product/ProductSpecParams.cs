using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.App.Application.Abstruction.Models.Product
{
    public class ProductSpecParams
    {
        public string? Sort { get; set; }
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }
        public int PageIndex { get; set; } = 1;
        private int pageSize = 5;
        private const int PageMaxSize = 10;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > PageMaxSize ? PageMaxSize : value; }
        }

    }
}
