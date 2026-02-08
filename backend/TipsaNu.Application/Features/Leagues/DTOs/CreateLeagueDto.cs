namespace TipsaNu.Application.Features.Leagues.DTOs
{
    public class CreateLeagueDto
    {
        public int TournamentId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? InvitationCode { get; set; }
        public int MaxMembers { get; set; }
    }
}
