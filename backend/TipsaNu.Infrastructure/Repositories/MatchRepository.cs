using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Interfaces;
using TipsaNu.Infrastructure.Presistence;
using Microsoft.EntityFrameworkCore;

namespace TipsaNu.Infrastructure.Repositories
{
    public class MatchRepository : IMatchRepository
    {
        private readonly AppDbContext _context;

        public MatchRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Match>> GetMatchesByGroupIdAsync(int groupId, CancellationToken cancellationToken = default)
        {
            return await _context.Matches
                .Where(m => m.GroupId == groupId)
                .Include(m => m.HomeCompetitor)
                .Include(m => m.AwayCompetitor)
                .Include(m => m.WinnerCompetitor)
                .OrderBy(m => m.StartTime)
                .ToListAsync(cancellationToken);
        }
        public async Task<Match?> GetMatchWithCompetitorsAsync(int matchId, CancellationToken cancellationToken = default)
        {
            return await _context.Matches
                .Include(m => m.HomeCompetitor)
                .Include(m => m.AwayCompetitor)
                .FirstOrDefaultAsync(m => m.MatchId == matchId, cancellationToken);
        }
    }
}
