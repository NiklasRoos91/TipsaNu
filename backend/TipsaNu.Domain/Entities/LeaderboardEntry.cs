namespace TipsaNu.Domain.Entities
{
    public class LeaderboardEntry
    {
        public int LeagueMemberId { get; set; }
        public int TotalPoints { get; set; }
        public DateTime LastUpdated { get; set; }

        // Navigation
        public LeagueMember LeagueMember { get; set; } = null!;
    }
}
}
