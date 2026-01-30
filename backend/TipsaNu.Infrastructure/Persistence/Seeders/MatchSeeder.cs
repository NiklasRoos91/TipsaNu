using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Enums;
using TipsaNu.Infrastructure.Presistence;

namespace TipsaNu.Infrastructure.Persistence.Seeders
{
    public static class MatchSeeder
    {
        public static async Task SeedAsync(AppDbContext context, Tournament tournament, List<Group> groups)
        {
            var matchDays = new[]
            {
                new DateTime(2026,6,11,13,0,0),
                new DateTime(2026,6,18,13,0,0),
                new DateTime(2026,6,24,13,0,0)
            };

            foreach (var group in groups)
            {
                var ids = context.GroupCompetitors.Where(gc => gc.GroupId == group.GroupId).Select(gc => gc.CompetitorId).ToList();
                int dateIndex = 0;
                for (int i = 0; i < ids.Count; i++)
                {
                    for (int j = i + 1; j < ids.Count; j++)
                    {
                        await context.Matches.AddAsync(new Match
                        {
                            TournamentId = tournament.TournamentId,
                            GroupId = group.GroupId,
                            HomeCompetitorId = ids[i],
                            AwayCompetitorId = ids[j],
                            MatchType = MatchTypeEnum.Group,
                            RoundNumber = 1,
                            StartTime = matchDays[dateIndex % matchDays.Length],
                            Status = MatchStatusEnum.Scheduled
                        });
                        dateIndex++;
                    }
                }
            }
            await context.SaveChangesAsync();
        }
    }
}
