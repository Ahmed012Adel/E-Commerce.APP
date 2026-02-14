using E_Commerce.App.Application.Abstruction;
using System.Security.Claims;

namespace E_Commerce.APIs.Servicies
{
    public class LoggedInUserService : ILoggedInUserService
    {
        private readonly IHttpContextAccessor? _httpContextAccessor;
        public string? UserId { get; }

       public LoggedInUserService(IHttpContextAccessor? httpContextAccessor)
       {
            _httpContextAccessor = httpContextAccessor;
            UserId = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.PrimarySid);
        }
    }
}
