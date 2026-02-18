using Microsoft.EntityFrameworkCore;
using TipsaNu.Domain.Interfaces;
using TipsaNu.Infrastructure.Presistence;

namespace TipsaNu.Infrastructure.Repositories
{
    public class LeaderboardRepository : ILeaderboardRepository
    {
        private readonly AppDbContext _context;

        public LeaderboardRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetTotalPointsForUserInTournamentAsync(int userId, int tournamentId, CancellationToken cancellationToken)
        {
            var predictionPoints = await _context.Predictions
                .Where(p => p.UserId == userId && p.Match.TournamentId == tournamentId)
                .SumAsync(p => (int?)p.PointsAwarded, cancellationToken) ?? 0;

            var extraBetPoints = await _context.ExtraBets
                .Where(e => e.UserId == userId && e.ExtraBetOption.TournamentId == tournamentId)
                .SumAsync(e => (int?)e.PointsAwarded, cancellationToken) ?? 0;

            return predictionPoints + extraBetPoints;
        }

        public async Task UpdateLeaderboardEntriesAsync(int userId, int tournamentId, int totalPoints, CancellationToken cancellationToken)
        {
            var entries = await _context.LeaderboardEntries
                .Include(le => le.LeagueMember)
                .ThenInclude(lm => lm.League)
                .Where(le => le.LeagueMember.UserId == userId && le.LeagueMember.League.TournamentId == tournamentId)
                .ToListAsync(cancellationToken);

            foreach (var entry in entries)
            {
                entry.TotalPoints = totalPoints;
                entry.LastUpdated = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
