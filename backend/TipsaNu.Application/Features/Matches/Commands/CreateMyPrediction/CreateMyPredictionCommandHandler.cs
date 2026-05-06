using MediatR;
using TipsaNu.Application.Commons.Interfaces;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Predictions.DTOs;
using TipsaNu.Application.Features.Predictions.Mappers;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.Features.Matches.Commands.CreateMyPrediction
{
    public class CreatePredictionHandler(
        IPredictionRepository predictionRepository,
        IGenericRepository<Prediction> genericPredictionRepository,
        IGenericRepository<Match> matchRepository,
        ICurrentUserService currentUser)
        : IRequestHandler<CreateMyPredictionCommand, OperationResult<MatchPredictionDto>>
    {
        public async Task<OperationResult<MatchPredictionDto>> Handle(CreateMyPredictionCommand request, CancellationToken cancellationToken)
        {
            var userId = currentUser.UserId;
            if (userId <= 0)
                return OperationResult<MatchPredictionDto>.Failure("Unauthorized");

            var match = await matchRepository.GetByIdAsync(request.MatchId, cancellationToken);
            if (match == null)
                return OperationResult<MatchPredictionDto>.Failure("Match not found");

            if (match.PredictionDeadline.HasValue && match.PredictionDeadline < DateTime.UtcNow)
                return OperationResult<MatchPredictionDto>.Failure("Prediction deadline has passed");

            var existingPrediction = await predictionRepository.GetByUserAndMatchAsync(userId, request.MatchId, cancellationToken);

            if (existingPrediction != null)
            {
                await genericPredictionRepository.DeleteAsync(existingPrediction.PredictionId, cancellationToken);
            }

            var prediction = new Prediction
            {
                MatchId = request.MatchId,
                UserId = userId,
                PredictedHomeScore = request.Prediction.PredictedHomeScore,
                PredictedAwayScore = request.Prediction.PredictedAwayScore,
                SubmittedAt = DateTime.UtcNow,
                PredictedWinnerId = request.Prediction.PredictedHomeScore > request.Prediction.PredictedAwayScore
                    ? match.HomeCompetitorId
                    : request.Prediction.PredictedAwayScore > request.Prediction.PredictedHomeScore
                        ? match.AwayCompetitorId
                        : null
            };

            await genericPredictionRepository.AddAsync(prediction, cancellationToken);

            prediction.Match = match;

            return OperationResult<MatchPredictionDto>.Success(prediction.ToMatchPredictionDto());
        }
    }
}