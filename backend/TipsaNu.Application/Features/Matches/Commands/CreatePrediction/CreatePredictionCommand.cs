using MediatR;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Matches.DTOs;

namespace TipsaNu.Application.Features.Matches.Commands.CreatePrediction
{
    public record CreatePredictionCommand(CreatePredictionRequestDto Prediction, int MatchId)
        : IRequest<OperationResult<PredictionDto>>;
}
