using AutoMapper;
using MediatR;
using TipsaNu.Application.Commons.Interfaces;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Predictions.DTOs;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.Features.Predictions.Queries.GetMyPredictionForMatch
{
    public class GetMyPredictionForMatchHandler
        : IRequestHandler<GetMyPredictionForMatchQuery, OperationResult<MatchPredictionDto>>
    {
        private readonly IPredictionRepository _predictionRepository;
        private readonly IMatchRepository _matchRepository;
        private readonly ICurrentUserService _currentUser;
        private readonly IMapper _mapper;

        public GetMyPredictionForMatchHandler(
            IPredictionRepository predictionRepository,
            IMatchRepository matchRepository,
            ICurrentUserService currentUser,
            IMapper mapper)
        {
            _predictionRepository = predictionRepository;
            _matchRepository = matchRepository;
            _currentUser = currentUser;
            _mapper = mapper;
        }

        public async Task<OperationResult<MatchPredictionDto>> Handle(
            GetMyPredictionForMatchQuery request,
            CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId;
            if (userId <= 0)
                return OperationResult<MatchPredictionDto>.Failure("Unauthorized");

            var match = await _matchRepository.GetMatchWithCompetitorsAsync(request.MatchId, cancellationToken);
            if (match == null)
                return OperationResult<MatchPredictionDto>.Failure("Match not found");

            var prediction = await _predictionRepository.GetPredictionForUserAndMatchAsync(userId, request.MatchId, cancellationToken);

            var dto = prediction != null
                ? _mapper.Map<MatchPredictionDto>(prediction)
                : new MatchPredictionDto
                {
                    MatchId = match.MatchId,
                    HomeTeamName = match.HomeCompetitor?.Name,
                    AwayTeamName = match.AwayCompetitor?.Name,
                    MatchStartTime = match.StartTime
                };

            return OperationResult<MatchPredictionDto>.Success(dto);
        }
    }
}