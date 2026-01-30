using TipsaNu.Domain.Enums;

namespace TipsaNu.Domain.Entities
{
    public class Match
    {
        public int MatchId { get; set; }
        public int TournamentId { get; set; }
        public int HomeCompetitorId { get; set; }
        public int AwayCompetitorId { get; set; }
        public int? GroupId { get; set; }
        public MatchTypeEnum MatchType { get; set; }
        public int RoundNumber { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? PredictionDeadline { get; set; }
        public int? ScoreHome { get; set; }
        public int? ScoreAway { get; set; }
        public int? WinnerCompetitorId { get; set; }
        public int? DependsOnMatch1Id { get; set; }
        public int? DependsOnMatch2Id { get; set; }
        public MatchStatusEnum Status { get; set; }

        // Navigation
        public Tournament Tournament { get; set; } = null!;
        public Competitor HomeCompetitor { get; set; } = null!;
        public Competitor AwayCompetitor { get; set; } = null!;
        public Competitor? WinnerCompetitor { get; set; }
        public Group? Group { get; set; }
        public Match? DependsOnMatch1 { get; set; }
        public Match? DependsOnMatch2 { get; set; }
        public ICollection<Prediction> Predictions { get; set; } = new List<Prediction>();
    }
}
