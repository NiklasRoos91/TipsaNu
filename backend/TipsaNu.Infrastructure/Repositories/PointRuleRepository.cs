using Microsoft.EntityFrameworkCore;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Enums;
using TipsaNu.Domain.Interfaces;
using TipsaNu.Infrastructure.Persistence;

namespace TipsaNu.Infrastructure.Repositories
{
    public class PointRuleRepository(AppDbContext context) : IPointRuleRepository
    {

        public async Task<List<PointRule>> GetPointRulesForTemplateAndMatchTypeAsync(
            int templateId,
            MatchTypeEnum matchType,
            CancellationToken cancellationToken = default)
        {
            return await context.PointRules
                .Where(pr => pr.TemplateId == templateId && pr.MatchType == matchType)
                .ToListAsync(cancellationToken);
        }
    }
}