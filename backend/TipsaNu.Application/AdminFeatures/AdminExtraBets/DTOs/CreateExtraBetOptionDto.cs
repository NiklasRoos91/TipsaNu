namespace TipsaNu.Application.AdminFeatures.AdminExtraBets.DTOs
{
    public class CreateExtraBetOptionDto
    {
        public int TournamentId { get; set; }
        public int? MatchId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Points { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public bool AllowCustomChoice { get; set; } = false;
        public List<string> Choices { get; set; } = new();
    }
}
