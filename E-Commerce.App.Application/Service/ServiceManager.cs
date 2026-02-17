using AutoMapper;
using E_Commerce.App.Application.Abstruction.Services;
using E_Commerce.App.Application.Abstruction.Services.Product;
using E_Commerce.App.Application.Service.ProductServicies;
using E_Commerce.App.Domain.Contract.Peresistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.App.Application.Service
{
    internal class ServiceManager : IServiceManager
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private Lazy<IproductServices> _ProductService;
        public ServiceManager(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;

            _ProductService = new Lazy<IproductServices>(() => new ProductService(_unitOfWork,_mapper)); 
        }
        public IproductServices ProductService => _ProductService.Value;
    }
}
