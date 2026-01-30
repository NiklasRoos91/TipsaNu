using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Enums;
using TipsaNu.Infrastructure.Presistence;

namespace TipsaNu.Infrastructure.Persistence.Seeders
{
    public static class TiebreakerSeeder
    {
        public static async Task SeedAsync(AppDbContext context, Tournament tournament)
        {
            var tiebreakers = new List<TournamentTiebreaker>
            {
                new TournamentTiebreaker { TournamentId = tournament.TournamentId, Criterion = TiebreakerCriterionEnum.GoalDifference, Priority = 1 },
                new TournamentTiebreaker { TournamentId = tournament.TournamentId, Criterion = TiebreakerCriterionEnum.GoalsScored, Priority = 2 },
                new TournamentTiebreaker { TournamentId = tournament.TournamentId, Criterion = TiebreakerCriterionEnum.HeadToHead, Priority = 3 },
                new TournamentTiebreaker { TournamentId = tournament.TournamentId, Criterion = TiebreakerCriterionEnum.Random, Priority = 4 }
            };
            await context.TournamentTiebreakers.AddRangeAsync(tiebreakers);
            await context.SaveChangesAsync();
        }
    }
}
