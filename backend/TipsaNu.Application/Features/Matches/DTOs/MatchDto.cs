using TipsaNu.Domain.Enums;

namespace TipsaNu.Application.Features.Matches.DTOs
{
    public class MatchDto
    {
        public int MatchId { get; set; }

        public int TournamentId { get; set; }
        public int? GroupId { get; set; }

        public MatchTypeEnum MatchType { get; set; }
        public int RoundNumber { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime? PredictionDeadline { get; set; }

        public int HomeCompetitorId { get; set; }
        public string HomeCompetitorName { get; set; } = null!;

        public int AwayCompetitorId { get; set; }
        public string AwayCompetitorName { get; set; } = null!;

        public int? ScoreHome { get; set; }
        public int? ScoreAway { get; set; }

        public int? WinnerCompetitorId { get; set; }
        public string? WinnerCompetitorName { get; set; }

        public MatchStatusEnum Status { get; set; }
    }
}