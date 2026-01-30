using TipsaNu.Domain.Entities;

namespace TipsaNu.Application.Features.Auth.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateToken(User user);
    }
}
