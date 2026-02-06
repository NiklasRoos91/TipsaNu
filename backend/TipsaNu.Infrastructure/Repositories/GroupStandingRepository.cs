using Microsoft.EntityFrameworkCore;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Interfaces;
using TipsaNu.Infrastructure.Presistence;

namespace TipsaNu.Infrastructure.Repositories
{
    public class GroupStandingRepository : IGroupStandingRepository
    {
        private readonly AppDbContext _context;

        public GroupStandingRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<GroupStanding>> GetGroupStandingsByGroupIdAsync(int groupId)
        {
            return await _context.GroupStandings
                                 .Where(s => s.GroupId == groupId)
                                 .Include(s => s.Competitor)
                                 .ToListAsync();
        }
    }
}
