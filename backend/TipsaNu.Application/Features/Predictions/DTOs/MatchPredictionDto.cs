namespace TipsaNu.Application.Features.Predictions.DTOs
{
    public class MatchPredictionDto
    {
        public int MatchId { get; set; }
        public int PredictedHomeScore { get; set; }
        public int PredictedAwayScore { get; set; }
        public int? PredictedWinnerId { get; set; }
        public int PointsAwarded { get; set; }
        public DateTime SubmittedAt { get; set; }

        // Match info
        public string? HomeTeamName { get; set; }
        public string? AwayTeamName { get; set; }
        public DateTime? MatchStartTime { get; set; }
    }
}
