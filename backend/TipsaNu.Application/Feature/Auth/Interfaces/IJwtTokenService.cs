using TipsaNu.Domain.Entities;

namespace TipsaNu.Application.Feature.Auth.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateToken(User user);
    }
}
