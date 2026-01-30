using TipsaNu.Domain.Enums;

namespace TipsaNu.Domain.Entities
{
    public class Tournament
    {
        public int TournamentId { get; set; }
        public int TemplateId { get; set; }
        public string Name { get; set; } = null!;
        public DateTime StartsAt { get; set; }
        public int CreatedByUserId { get; set; }
        public TournamentStatusEnum Status { get; set; }

        // Navigation
        public TournamentTemplate Template { get; set; } = null!;
        public User CreatedByUser { get; set; } = null!;
        public ICollection<Group> Groups { get; set; } = new List<Group>();
        public ICollection<Match> Matches { get; set; } = new List<Match>();
        public ICollection<League> Leagues { get; set; } = new List<League>();
        public ICollection<TournamentTiebreaker> TournamentTiebreakers { get; set; } = new List<TournamentTiebreaker>();
        public ICollection<ExtraBetOption> ExtraBetOptions { get; set; } = new List<ExtraBetOption>();
    }
}
