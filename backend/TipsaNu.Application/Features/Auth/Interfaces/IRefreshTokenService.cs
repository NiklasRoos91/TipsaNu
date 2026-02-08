using TipsaNu.Domain.Entities;

namespace TipsaNu.Application.Features.Auth.Interfaces
{
    public interface IRefreshTokenService
    {
        Task<RefreshToken> CreateRefreshTokenAsync(User user, CancellationToken cancellationToken = default);
        Task<RefreshToken?> GetRefreshTokenAsync(string token, CancellationToken cancellationToken = default);
        Task RevokeRefreshTokenAsync(RefreshToken token, CancellationToken cancellationToken = default);
    }
}
