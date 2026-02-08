using Microsoft.EntityFrameworkCore;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Enums;
using TipsaNu.Domain.Interfaces;
using TipsaNu.Infrastructure.Presistence;

namespace TipsaNu.Infrastructure.Repositories
{
    public class PointRuleRepository : IPointRuleRepository
    {
        private readonly AppDbContext _context;

        public PointRuleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<PointRule>> GetPointRulesForTemplateAndMatchTypeAsync(
            int templateId,
            MatchTypeEnum matchType,
            CancellationToken cancellationToken = default)
        {
            return await _context.PointRules
                .Where(pr => pr.TemplateId == templateId && pr.MatchType == matchType)
                .ToListAsync(cancellationToken);
        }
    }
}