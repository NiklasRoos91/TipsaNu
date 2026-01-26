namespace TipsaNu.Domain.Entities
{
    public class Prediction
    {
        public int PredictionId { get; set; }
        public int UserId { get; set; }
        public int MatchId { get; set; }
        public int PredictedHomeScore { get; set; }
        public int PredictedAwayScore { get; set; }
        public int? PredictedWinnerId { get; set; }
        public int PointsAwarded { get; set; }
        public DateTime SubmittedAt { get; set; }

        // Navigation
        public User User { get; set; } = null!;
        public Match Match { get; set; } = null!;
        public Competitor? PredictedWinner { get; set; }
    }
}
