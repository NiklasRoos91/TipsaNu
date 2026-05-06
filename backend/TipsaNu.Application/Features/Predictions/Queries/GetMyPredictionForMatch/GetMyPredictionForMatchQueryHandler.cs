using MediatR;
using TipsaNu.Application.Commons.Interfaces;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Predictions.DTOs;
using TipsaNu.Application.Features.Predictions.Mappers;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.Features.Predictions.Queries.GetMyPredictionForMatch
{
    public class GetMyPredictionForMatchHandler(
        IPredictionRepository predictionRepository,
        IMatchRepository matchRepository,
        ICurrentUserService currentUser)
        : IRequestHandler<GetMyPredictionForMatchQuery, OperationResult<MatchPredictionDto>>
    {
        public async Task<OperationResult<MatchPredictionDto>> Handle(
            GetMyPredictionForMatchQuery request,
            CancellationToken cancellationToken)
        {
            var userId = currentUser.UserId;
            if (userId <= 0)
                return OperationResult<MatchPredictionDto>.Failure("Unauthorized");

            var match = await matchRepository.GetMatchWithCompetitorsAsync(request.MatchId, cancellationToken);
            if (match == null)
                return OperationResult<MatchPredictionDto>.Failure("Match not found");

            var prediction = await predictionRepository.GetPredictionForUserAndMatchAsync(userId, request.MatchId, cancellationToken);

            MatchPredictionDto dto;

            if (prediction != null)
            {
                dto = prediction.ToMatchPredictionDto();
            }
            else
            {
                dto = new MatchPredictionDto
                    {
                        MatchId = match.MatchId,
                        HomeTeamName = match.HomeCompetitor?.Name,
                        AwayTeamName = match.AwayCompetitor?.Name,
                        MatchStartTime = match.StartTime
                    };
            }

            return OperationResult<MatchPredictionDto>.Success(dto);
        }
    }
}