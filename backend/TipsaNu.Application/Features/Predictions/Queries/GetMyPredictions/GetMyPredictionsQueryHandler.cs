using MediatR;
using TipsaNu.Application.Commons.Interfaces;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Predictions.DTOs;
using TipsaNu.Application.Features.Predictions.Mappers;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.Features.Predictions.Queries.GetMyPredictions
{
    public class GetMyPredictionsQueryHandler(
        IPredictionRepository predictionRepository,
        ICurrentUserService currentUser)
        : IRequestHandler<GetMyPredictionsQuery, OperationResult<List<MatchPredictionDto>>>
    {
        public async Task<OperationResult<List<MatchPredictionDto>>> Handle(GetMyPredictionsQuery request, CancellationToken cancellationToken)
        {
            var userId = currentUser.UserId;
            if (userId <= 0)
                return OperationResult<List<MatchPredictionDto>>.Failure("Unauthorized");

            var predictions = await predictionRepository
                .GetPredictionsForUserWithMatchAsync(userId, cancellationToken);

            predictions.ForEach(p => p.Match = p.Match);

            var dtos = predictions
                .Select(p => p.ToMatchPredictionDto())
                .ToList();

            return OperationResult<List<MatchPredictionDto>>.Success(dtos);
        }
    }
}
