namespace TipsaNu.Domain.Entities
{
    public class ExtraBet
    {
        public int ExtraBetId { get; set; }
        public int UserId { get; set; }
        public int OptionId { get; set; }
        public string Value { get; set; } = null!;
        public int? PointsAwarded { get; set; }
        public DateTime SubmittedAt { get; set; }

        // Navigation
        public User User { get; set; } = null!;
        public ExtraBetOption ExtraBetOption { get; set; } = null!;
    }
}
