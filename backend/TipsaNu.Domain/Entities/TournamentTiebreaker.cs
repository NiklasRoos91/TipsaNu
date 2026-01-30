using TipsaNu.Domain.Enums;

namespace TipsaNu.Domain.Entities
{
    public class TournamentTiebreaker
    {
        public int TiebreakerId { get; set; }
        public int TournamentId { get; set; }
        public TiebreakerCriterionEnum Criterion { get; set; }
        public int Priority { get; set; }

        // Navigation
        public Tournament Tournament { get; set; } = null!;
    }
}
