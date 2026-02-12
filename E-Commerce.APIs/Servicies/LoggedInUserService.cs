using E_Commerce.App.Application.Abstruction;
using System.Security.Claims;

namespace E_Commerce.APIs.Servicies
{
    public class LoggedInUserService : ILoggedInUserService
    {
        private readonly HttpContextAccessor? _httpContextAccessor;
        public string? UserId { get; }

       public LoggedInUserService(HttpContextAccessor? httpContextAccessor)
       {
            _httpContextAccessor = httpContextAccessor;
            UserId = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.PrimarySid);
        }
    }
}
