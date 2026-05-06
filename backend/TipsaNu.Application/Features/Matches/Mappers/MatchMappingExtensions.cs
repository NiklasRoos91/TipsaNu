using TipsaNu.Application.Features.Matches.DTOs;
using TipsaNu.Application.AdminFeatures.AdminMatches.DTOs;
using TipsaNu.Domain.Enums;
using Match = TipsaNu.Domain.Entities.Match;

namespace TipsaNu.Application.Features.Matches.Mappers
{
    public static class MatchMappingExtensions
    {
        public static Match ToEntity(this CreateMatchDto dto)
        {
            return new Match
            {
                TournamentId = dto.TournamentId,
                HomeCompetitorId = dto.HomeCompetitorId,
                AwayCompetitorId = dto.AwayCompetitorId,
                MatchType = dto.MatchType,
                RoundNumber = dto.RoundNumber,
                GroupId = dto.GroupId,
                StartTime = dto.StartTime,
                PredictionDeadline = dto.PredictionDeadline ?? dto.StartTime,
                Status = MatchStatusEnum.Scheduled
            };
        }

        public static MatchDto ToMatchDto(this Match entity)
        {
            return new MatchDto
            {
                MatchId = entity.MatchId,
                TournamentId = entity.TournamentId,
                GroupId = entity.GroupId,
                MatchType = entity.MatchType,
                RoundNumber = entity.RoundNumber,
                StartTime = entity.StartTime,
                PredictionDeadline = entity.PredictionDeadline,
                
                HomeCompetitorId = entity.HomeCompetitorId,
                HomeCompetitorName = entity.HomeCompetitor?.Name ?? "Unknown",
                
                AwayCompetitorId = entity.AwayCompetitorId,
                AwayCompetitorName = entity.AwayCompetitor?.Name ?? "Unknown",

                ScoreHome = entity.ScoreHome,
                ScoreAway = entity.ScoreAway,

                WinnerCompetitorId = entity.WinnerCompetitorId,
                WinnerCompetitorName = entity.WinnerCompetitor?.Name,

                Status = entity.Status
            };
        }
    }
}