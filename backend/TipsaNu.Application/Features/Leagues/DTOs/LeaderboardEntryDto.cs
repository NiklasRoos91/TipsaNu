namespace TipsaNu.Application.Features.Leagues.DTOs
{
    public class LeaderboardEntryDto
    {
        public int LeagueMemberId { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public int TotalPoints { get; set; }
    }
}
