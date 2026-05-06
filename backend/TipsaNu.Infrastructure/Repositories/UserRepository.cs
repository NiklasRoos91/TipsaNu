using Microsoft.EntityFrameworkCore;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Interfaces;
using TipsaNu.Infrastructure.Persistence;

namespace TipsaNu.Infrastructure.Repositories
{

    public class UserRepository(AppDbContext context) : IUserRepository
    {
        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        }
    }
}