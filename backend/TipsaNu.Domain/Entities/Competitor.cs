namespace TipsaNu.Domain.Entities
{
    public class Competitor
    {
        public int CompetitorId { get; set; }
        public string Name { get; set; } = null!;
        public bool IsIndividual { get; set; }

        // Navigation
        public ICollection<GroupCompetitor> GroupCompetitors { get; set; } = new List<GroupCompetitor>();
        public ICollection<GroupStanding> GroupStandings { get; set; } = new List<GroupStanding>();
        public ICollection<Match> HomeMatches { get; set; } = new List<Match>();
        public ICollection<Match> AwayMatches { get; set; } = new List<Match>();
        public ICollection<Match> WinningMatches { get; set; } = new List<Match>();
    }
}
