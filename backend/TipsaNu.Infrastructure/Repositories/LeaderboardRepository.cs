using Microsoft.EntityFrameworkCore;
using TipsaNu.Domain.Interfaces;
using TipsaNu.Infrastructure.Persistence;

namespace TipsaNu.Infrastructure.Repositories
{
    public class LeaderboardRepository(AppDbContext context) : ILeaderboardRepository
    {
        public async Task<int> GetTotalPointsForUserInTournamentAsync(int userId, int tournamentId, CancellationToken cancellationToken)
        {
            var predictionPoints = await context.Predictions
                .Where(p => p.UserId == userId && p.Match.TournamentId == tournamentId)
                .SumAsync(p => (int?)p.PointsAwarded, cancellationToken) ?? 0;

            var extraBetPoints = await context.ExtraBets
                .Where(e => e.UserId == userId && e.ExtraBetOption.TournamentId == tournamentId)
                .SumAsync(e => (int?)e.PointsAwarded, cancellationToken) ?? 0;

            return predictionPoints + extraBetPoints;
        }

        public async Task UpdateLeaderboardEntriesAsync(int userId, int tournamentId, int totalPoints, CancellationToken cancellationToken)
        {
            await context.LeaderboardEntries
                .Where(le => le.LeagueMember.UserId == userId && le.LeagueMember.League.TournamentId == tournamentId)
                .ExecuteUpdateAsync(setters => setters
                        .SetProperty(le => le.TotalPoints, totalPoints)
                        .SetProperty(le => le.LastUpdated, DateTime.UtcNow), 
                    cancellationToken);
        }
    }
}
