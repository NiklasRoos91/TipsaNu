using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Enums;

namespace TipsaNu.Domain.Interfaces
{
    public interface IPointRuleRepository
    {
        Task<List<PointRule>> GetPointRulesForTemplateAndMatchTypeAsync(
            int templateId,
            MatchTypeEnum matchType,
            CancellationToken cancellationToken = default);
    }
}
