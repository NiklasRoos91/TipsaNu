namespace TipsaNu.Domain.Entities
{
    public class ExtraBetOptionCorrectValue
    {
        public int CorrectValueId { get; set; }
        public int OptionId { get; set; }
        public string Value { get; set; } = null!;

        // Navigation
        public ExtraBetOption ExtraBetOption { get; set; } = null!;
    }
}
