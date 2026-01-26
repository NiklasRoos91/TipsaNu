namespace TipsaNu.Domain.Entities
{
    public class LeagueMember
    {
        public int LeagueMemberId { get; set; }
        public int LeagueId { get; set; }
        public int UserId { get; set; }
        public DateTime JoinedAt { get; set; }

        // Navigation
        public League League { get; set; } = null!;
        public User User { get; set; } = null!;
        public LeaderboardEntry LeaderboardEntry { get; set; } = null!;
    }
}
