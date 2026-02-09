namespace TipsaNu.Application.Features.Leagues.DTOs
{
    public class LeagueWithLeaderboardDto
    {
        public int LeagueId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string InvitationCode { get; set; } = null!;
        public int MaxMembers { get; set; }
        public int CurrentMembers { get; set; }
        public int AdminUserId { get; set; }
        public string AdminUsername { get; set; } = null!;

        // 
        public List<LeaderboardEntryDto> Leaderboard { get; set; } = new();
    }
}
