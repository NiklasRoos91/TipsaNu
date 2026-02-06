using Microsoft.EntityFrameworkCore;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Interfaces;
using TipsaNu.Infrastructure.Presistence;

namespace TipsaNu.Infrastructure.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly AppDbContext _context;

        public GroupRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Group>> GetGroupsByTournamentIdAsync(int tournamentId)
        {
            return await _context.Groups
                                 .Where(g => g.TournamentId == tournamentId)
                                 .ToListAsync();
        }
    }
}
