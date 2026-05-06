using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TipsaNu.Application.Commons.Interfaces;

namespace TipsaNu.Infrastructure.Services
{
    public sealed class CurrentUserService(IHttpContextAccessor http) : ICurrentUserService
    {
        public int UserId
        {
            get
            {
                var user = http.HttpContext?.User;
                if (user == null || !user.Identity!.IsAuthenticated)
                    return 0;

                var sub = user.FindFirstValue(JwtRegisteredClaimNames.Sub);

                if (string.IsNullOrEmpty(sub))
                    sub = user.FindFirstValue(ClaimTypes.NameIdentifier);

                return int.TryParse(sub, out var userId) ? userId : 0;
            }
        }
    }
}
