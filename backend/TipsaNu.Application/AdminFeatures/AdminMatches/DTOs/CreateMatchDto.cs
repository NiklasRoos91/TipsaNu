using TipsaNu.Domain.Enums;

namespace TipsaNu.Application.AdminFeatures.AdminMatches.DTOs 
{
    public class CreateMatchDto
    {
        public int TournamentId { get; set; }
        public int HomeCompetitorId { get; set; }
        public int AwayCompetitorId { get; set; }
        
        public MatchTypeEnum MatchType { get; set; }
        
        public int RoundNumber { get; set; }
        
        public int? GroupId { get; set; }
        
        public DateTime StartTime { get; set; }
        public DateTime? PredictionDeadline { get; set; }
    }
}