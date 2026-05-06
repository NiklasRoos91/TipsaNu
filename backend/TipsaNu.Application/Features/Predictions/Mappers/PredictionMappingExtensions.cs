using TipsaNu.Application.Features.Predictions.DTOs;
using TipsaNu.Domain.Entities;

namespace TipsaNu.Application.Features.Predictions.Mappers;

public static class PredictionMappingExtensions
{
    public static MatchPredictionDto ToMatchPredictionDto(this Prediction entity)
    {
        return new MatchPredictionDto
        {
            MatchId = entity.MatchId,
            PredictedHomeScore = entity.PredictedHomeScore,
            PredictedAwayScore = entity.PredictedAwayScore,
            PredictedWinnerId = entity.PredictedWinnerId,
            PointsAwarded = entity.PointsAwarded,
            SubmittedAt = entity.SubmittedAt,

            HomeTeamName = entity.Match?.HomeCompetitor?.Name ?? "Unknown",
            AwayTeamName = entity.Match?.AwayCompetitor?.Name ?? "Unknown",
            MatchStartTime = entity.Match?.StartTime
        };
    }
}