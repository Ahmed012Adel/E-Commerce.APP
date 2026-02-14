using E_Commerce.App.Application.Abstruction.Services.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.App.Application.Abstruction.Services
{
    public interface IServiceManager
    {
        public IproductServices ProductService {  get; }
    }
}
