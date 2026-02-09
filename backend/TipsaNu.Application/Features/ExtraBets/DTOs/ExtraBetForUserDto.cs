namespace TipsaNu.Application.Features.ExtraBets.DTOs
{
    public class ExtraBetForUserDto
    {
        public int ExtraBetId { get; set; }
        public string Value { get; set; } = null!;
        public int? PointsAwarded { get; set; }
        public DateTime SubmittedAt { get; set; }
    }
}