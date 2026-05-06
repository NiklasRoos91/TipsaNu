using Microsoft.EntityFrameworkCore;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Interfaces;
using TipsaNu.Infrastructure.Persistence;

namespace TipsaNu.Infrastructure.Repositories
{
    public class GroupStandingRepository(AppDbContext context) : IGroupStandingRepository
    {
        public async Task<List<GroupStanding>> GetGroupStandingsByGroupIdAsync(int groupId, CancellationToken cancellationToken = default)
        {
            return await context.GroupStandings
                                 .Where(s => s.GroupId == groupId)
                                 .Include(s => s.Competitor)
                                 .ToListAsync(cancellationToken);
        }
    }
}
