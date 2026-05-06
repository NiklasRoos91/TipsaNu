using Microsoft.EntityFrameworkCore;
using TipsaNu.Application.Features.Auth.Interfaces;
using TipsaNu.Domain.Entities;
using TipsaNu.Infrastructure.Persistence;

namespace TipsaNu.Infrastructure.Auth
{
    public class RefreshTokenService(AppDbContext db) : IRefreshTokenService
    {
        public async Task<RefreshToken> CreateRefreshTokenAsync(User user, CancellationToken cancellationToken = default)
        {
            var token = new RefreshToken
            {
                UserId = user.UserId,
                Token = Convert.ToBase64String(Guid.NewGuid().ToByteArray()),
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                CreatedAt = DateTime.UtcNow,
                Revoked = false
            };

            db.RefreshTokens.Add(token);
            await db.SaveChangesAsync(cancellationToken);
            return token;
        }

        public async Task<RefreshToken?> GetRefreshTokenAsync(string token, CancellationToken cancellationToken = default)
        {
            return await db.RefreshTokens
                .Include(rt => rt.User)
                .FirstOrDefaultAsync(rt =>
                    rt.Token == token &&
                    !rt.Revoked &&
                    rt.ExpiresAt > DateTime.UtcNow, cancellationToken);
        }

        public async Task RevokeRefreshTokenAsync(RefreshToken token, CancellationToken cancellationToken = default)
        {
            token.Revoked = true;
            await db.SaveChangesAsync(cancellationToken);
        }
    }
}
