using TipsaNu.Domain.Enums;

namespace TipsaNu.Domain.Entities
{
    public class PointRule
    {
        public int PointRuleId { get; set; }
        public int TemplateId { get; set; }
        public MatchTypeEnum MatchType { get; set; }
        public TippingCriterionEnum Criterion { get; set; }
        public int Points { get; set; }

        // Navigation
        public TournamentTemplate Template { get; set; } = null!;
    }
}
