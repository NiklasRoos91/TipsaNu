using MediatR;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Predictions.DTOs;

namespace TipsaNu.Application.Features.Predictions.Queries.GetMyPredictions
{
    public record GetMyPredictionsQuery() : IRequest<OperationResult<List<MatchPredictionDto>>>;
}
