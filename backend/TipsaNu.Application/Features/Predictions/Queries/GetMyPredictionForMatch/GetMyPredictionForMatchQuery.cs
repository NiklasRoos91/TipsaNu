using MediatR;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Predictions.DTOs;

namespace TipsaNu.Application.Features.Predictions.Queries.GetMyPredictionForMatch
{
    public record GetMyPredictionForMatchQuery(int MatchId)
        : IRequest<OperationResult<MatchPredictionDto>>;
}
