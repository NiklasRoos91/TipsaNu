namespace TipsaNu.Domain.Entities
{
    public class GroupStanding
    {
        public int StandingId { get; set; }
        public int GroupId { get; set; }
        public int CompetitorId { get; set; }
        public int Rank { get; set; }
        public int Points { get; set; }
        public int Played { get; set; }
        public int Won { get; set; }
        public int Draw { get; set; }
        public int Lost { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
        public int GoalDifference { get; set; }

        // Navigation
        public Group Group { get; set; } = null!;
        public Competitor Competitor { get; set; } = null!;
    }
}
