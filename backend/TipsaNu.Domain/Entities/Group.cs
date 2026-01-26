namespace TipsaNu.Domain.Entities
{
    public class Group
    {
        public int GroupId { get; set; }
        public int TournamentId { get; set; }
        public string Name { get; set; } = null!;
        public int MaxTeams { get; set; }

        // Navigation
        public Tournament Tournament { get; set; } = null!;
        public ICollection<GroupCompetitor> GroupCompetitors { get; set; } = new List<GroupCompetitor>();
        public ICollection<Match> Matches { get; set; } = new List<Match>();
        public ICollection<GroupStanding> Standings { get; set; } = new List<GroupStanding>();
    }
}
