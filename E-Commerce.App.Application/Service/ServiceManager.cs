using AutoMapper;
using E_Commerce.App.Application.Abstruction.Services;
using E_Commerce.App.Application.Abstruction.Services.Auth;
using E_Commerce.App.Application.Abstruction.Services.Basket;
using E_Commerce.App.Application.Abstruction.Services.Product;
using E_Commerce.App.Application.Service.ProductServicies;
using E_Commerce.App.Domain.Contract.Peresistence;
using Microsoft.Extensions.Configuration;

namespace E_Commerce.App.Application.Service
{
    internal class ServiceManager : IServiceManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        private readonly Lazy<IproductServices> _productService;
        private readonly Lazy<IBasketService> _basketService;
        private readonly Lazy<IAuthService> _authService;
        public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration, Func<IBasketService> basketServiceFactory , Func<IAuthService> AuthServiceFactory)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _configuration = configuration;

            _productService = new Lazy<IproductServices>(() => new ProductService(_unitOfWork, _mapper));
            _basketService = new Lazy<IBasketService>(basketServiceFactory);
            _authService = new Lazy<IAuthService>(AuthServiceFactory);
        }
        public IproductServices ProductService => _productService.Value;

        public IBasketService BasketService => _basketService.Value;

        public IAuthService AuthService => _authService.Value;
    }
}