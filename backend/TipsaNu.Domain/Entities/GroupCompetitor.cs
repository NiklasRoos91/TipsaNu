namespace TipsaNu.Domain.Entities
{
    public class GroupCompetitor
    {
        public int GroupCompetitorId { get; set; }
        public int GroupId { get; set; }
        public int CompetitorId { get; set; }

        // Navigation
        public Group Group { get; set; } = null!;
        public Competitor Competitor { get; set; } = null!;
    }
}
