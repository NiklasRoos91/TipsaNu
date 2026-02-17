namespace TipsaNu.Application.Features.ExtraBets.DTOs
{
    public class ExtraBetDto
    {
        public int ExtraBetId { get; set; }
        public int OptionId { get; set; }

        public int? ChoiceId { get; set; }
        public string? CustomValue { get; set; }
        public int? PointsAwarded { get; set; }
        public DateTime SubmittedAt { get; set; }
    }
}
