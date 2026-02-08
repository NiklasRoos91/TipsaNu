using Microsoft.EntityFrameworkCore;
using TipsaNu.Application.Features.Auth.Interfaces;
using TipsaNu.Domain.Entities;
using TipsaNu.Infrastructure.Presistence;

namespace TipsaNu.Infrastructure.Auth
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly AppDbContext _db;

        public RefreshTokenService(AppDbContext db)
        {
            _db = db;
        }

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

            _db.RefreshTokens.Add(token);
            await _db.SaveChangesAsync(cancellationToken);
            return token;
        }

        public async Task<RefreshToken?> GetRefreshTokenAsync(string token, CancellationToken cancellationToken = default)
        {
            return await _db.RefreshTokens
                .Include(rt => rt.User)
                .FirstOrDefaultAsync(rt =>
                    rt.Token == token &&
                    !rt.Revoked &&
                    rt.ExpiresAt > DateTime.UtcNow, cancellationToken);
        }

        public async Task RevokeRefreshTokenAsync(RefreshToken token, CancellationToken cancellationToken = default)
        {
            token.Revoked = true;
            await _db.SaveChangesAsync(cancellationToken);
        }
    }
}
