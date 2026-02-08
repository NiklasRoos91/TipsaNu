using Microsoft.EntityFrameworkCore;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Interfaces;
using TipsaNu.Infrastructure.Presistence;

namespace TipsaNu.Infrastructure.Repositories
{

    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        }
    }
}