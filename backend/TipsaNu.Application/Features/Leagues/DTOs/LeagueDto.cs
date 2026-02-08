namespace TipsaNu.Application.Features.Leagues.DTOs
{
    public class LeagueDto
    {
        public int LeagueId { get; set; }
        public int TournamentId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string InvitationCode { get; set; } = null!;
        public int AdminUserId { get; set; }
        public int? MaxMembers { get; set; }
    }
}
