
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Enums;
using TipsaNu.Infrastructure.Presistence;

namespace TipsaNu.Infrastructure.Persistence.Seeders
{
    public static class PointRuleSeeder
    {
        public static async Task SeedAsync(AppDbContext context, Tournament tournament)
        {
            var templateId = (await context.Tournaments.FindAsync(tournament.TournamentId))!.TemplateId;

            var pointRules = new List<PointRule>
            {
                new PointRule { TemplateId = templateId, MatchType = MatchTypeEnum.Group, Criterion = TippingCriterionEnum.CorrectOutcome, Points = 3 },
                new PointRule { TemplateId = templateId, MatchType = MatchTypeEnum.Group, Criterion = TippingCriterionEnum.ExactScore, Points = 5 },
                new PointRule { TemplateId = templateId, MatchType = MatchTypeEnum.Group, Criterion = TippingCriterionEnum.GoalDifference, Points = 1 },
                new PointRule { TemplateId = templateId, MatchType = MatchTypeEnum.Group, Criterion = TippingCriterionEnum.Other, Points = 0 }
            };
            await context.PointRules.AddRangeAsync(pointRules);
            await context.SaveChangesAsync();
        }
    }
}
