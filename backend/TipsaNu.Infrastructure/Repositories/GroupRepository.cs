using Microsoft.EntityFrameworkCore;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Interfaces;
using TipsaNu.Infrastructure.Persistence;

namespace TipsaNu.Infrastructure.Repositories
{
    public class GroupRepository(AppDbContext context) : IGroupRepository
    {
        public async Task<IEnumerable<Group>> GetGroupsByTournamentIdAsync(int tournamentId, CancellationToken cancellationToken = default)
        {
            return await context.Groups
                                 .Where(g => g.TournamentId == tournamentId)
                                 .ToListAsync(cancellationToken);
        }
    }
}
