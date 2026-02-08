namespace TipsaNu.Application.Features.Matches.DTOs
{
    public class PredictionDto
    {
        public int UserId { get; set; }
        public int PredictedHomeScore { get; set; }
        public int PredictedAwayScore { get; set; }
        public int? PredictedWinnerId { get; set; }
        public int PointsAwarded { get; set; }
        public DateTime SubmittedAt { get; set; }
    }
}
