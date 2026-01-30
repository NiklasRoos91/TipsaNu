namespace TipsaNu.Domain.Entities
{
    public class ExtraBetOptionChoice
    {
        public int ChoiceId { get; set; }
        public int OptionId { get; set; }
        public string Value { get; set; } = null!;

        // Navigation
        public ExtraBetOption ExtraBetOption { get; set; } = null!;
    }
}
