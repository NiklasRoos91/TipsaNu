using TipsaNu.Domain.Entities;

namespace TipsaNu.Infrastructure.Persistence.Seeders
{
    public static class GroupSeeder
    {
        public static async Task<List<Group>> SeedAsync(AppDbContext context, Tournament tournament, List<Competitor> competitors)
        {
            var groupLabels = new[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L" };
            var groups = groupLabels.Select(label => new Group
            {
                Name = $"Group {label}",
                TournamentId = tournament.TournamentId,
                MaxTeams = 4
            }).ToList();

            await context.Groups.AddRangeAsync(groups);
            await context.SaveChangesAsync();

            int compIndex = 0;
            foreach (var group in groups)
            {
                for (int i = 0; i < 4; i++)
                {
                    var competitorId = competitors[compIndex++].CompetitorId;

                    await context.GroupCompetitors.AddAsync(new GroupCompetitor
                    {
                        GroupId = group.GroupId,
                        CompetitorId = competitorId
                    });

                    await context.TournamentCompetitors.AddAsync(new TournamentCompetitor
                    {
                        TournamentId = tournament.TournamentId,
                        CompetitorId = competitorId
                    });
                }
            }

            await context.SaveChangesAsync();
            return groups;
        }
    }
}
