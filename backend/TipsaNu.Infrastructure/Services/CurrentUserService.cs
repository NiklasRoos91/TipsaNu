using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TipsaNu.Application.Commons.Interfaces;

namespace TipsaNu.Infrastructure.Services
{
    public sealed class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _http;

        public CurrentUserService(IHttpContextAccessor http)
        {
            _http = http;
        }

        public int UserId
        {
            get
            {
                var user = _http.HttpContext?.User;
                var id = user?.FindFirstValue(ClaimTypes.NameIdentifier);

                return int.TryParse(id, out var userId) ? userId : 0;
            }
        }
    }
}
