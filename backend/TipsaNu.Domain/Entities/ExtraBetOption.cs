namespace TipsaNu.Domain.Entities
{
    public class ExtraBetOption
    {
        public int OptionId { get; set; }
        public int TournamentId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Points { get; set; }
        public int? MatchId { get; set; }
        public DateTime? ExpiresAt { get; set; }

        // Navigation
        public Tournament Tournament { get; set; } = null!;
        public Match? Match { get; set; }
        public ICollection<ExtraBet> ExtraBets { get; set; } = new List<ExtraBet>();
        public ICollection<ExtraBetOptionChoice> ExtraBetOptionChoices { get; set; } = new List<ExtraBetOptionChoice>();
        public ICollection<ExtraBetOptionCorrectValue> ExtraBetOptionCorrectValues { get; set; } = new List<ExtraBetOptionCorrectValue>();
    }
}
