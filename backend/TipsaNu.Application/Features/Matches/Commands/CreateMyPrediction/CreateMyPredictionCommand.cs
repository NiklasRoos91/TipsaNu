using MediatR;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Matches.DTOs;

namespace TipsaNu.Application.Features.Matches.Commands.CreateMyPrediction
{
    public record CreateMyPredictionCommand(CreateMyPredictionRequestDto Prediction, int MatchId)
        : IRequest<OperationResult<PredictionDto>>;
}
