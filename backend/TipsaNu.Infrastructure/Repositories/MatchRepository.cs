using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using TipsaNu.Infrastructure.Persistence;

namespace TipsaNu.Infrastructure.Repositories
{
    public class MatchRepository(AppDbContext context) : IMatchRepository
    {

        public async Task<List<Match>> GetMatchesByGroupIdAsync(int groupId, CancellationToken cancellationToken = default)
        {
            return await context.Matches
                .Where(m => m.GroupId == groupId)
                .Include(m => m.HomeCompetitor)
                .Include(m => m.AwayCompetitor)
                .Include(m => m.WinnerCompetitor)
                .OrderBy(m => m.StartTime)
                .ToListAsync(cancellationToken);
        }
        public async Task<Match?> GetMatchWithCompetitorsAsync(int matchId, CancellationToken cancellationToken = default)
        {
            return await context.Matches
                .Include(m => m.HomeCompetitor)
                .Include(m => m.AwayCompetitor)
                .FirstOrDefaultAsync(m => m.MatchId == matchId, cancellationToken);
        }
        public async Task<Match?> GetMatchWithTournamentAsync(int matchId, CancellationToken cancellationToken = default)
        {
            return await context.Matches
                .Include(m => m.Tournament)
                .FirstOrDefaultAsync(m => m.MatchId == matchId, cancellationToken);
        }
    }
}
