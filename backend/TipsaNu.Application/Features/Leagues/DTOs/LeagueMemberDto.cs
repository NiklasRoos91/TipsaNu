namespace TipsaNu.Application.Features.Leagues.DTOs
{
    public class LeagueMemberDto
    {
        public int LeagueMemberId { get; set; }
        public int LeagueId { get; set; }
        public int UserId { get; set; }
        public DateTime JoinedAt { get; set; }
    }
}
