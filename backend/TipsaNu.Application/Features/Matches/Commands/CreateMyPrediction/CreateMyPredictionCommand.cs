using MediatR;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Predictions.DTOs;

namespace TipsaNu.Application.Features.Matches.Commands.CreateMyPrediction
{
    public record CreateMyPredictionCommand(CreateMyPredictionRequestDto Prediction, int MatchId)
        : IRequest<OperationResult<MatchPredictionDto>>;
}
