using AutoMapper;
using MediatR;
using TipsaNu.Application.Commons.Interfaces;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Matches.DTOs;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.Features.Matches.Commands.CreateMyPrediction
{
    public class CreatePredictionHandler : IRequestHandler<CreateMyPredictionCommand, OperationResult<PredictionDto>>
    {
        private readonly IGenericRepository<Prediction> _predictionRepository;
        private readonly IGenericRepository<Match> _matchRepository;
        private readonly ICurrentUserService _currentUser;
        private readonly IMapper _mapper;

        public CreatePredictionHandler(
            IGenericRepository<Prediction> predictionRepository,
            IGenericRepository<Match> matchRepository,
            ICurrentUserService currentUser,
            IMapper mapper)
        {
            _predictionRepository = predictionRepository;
            _matchRepository = matchRepository;
            _currentUser = currentUser;
            _mapper = mapper;
        }

        public async Task<OperationResult<PredictionDto>> Handle(CreateMyPredictionCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId;
            if (userId <= 0)
                return OperationResult<PredictionDto>.Failure("Unauthorized");

            var match = await _matchRepository.GetByIdAsync(request.MatchId);
            if (match == null)
                return OperationResult<PredictionDto>.Failure("Match not found");

            if (match.PredictionDeadline.HasValue && match.PredictionDeadline < DateTime.UtcNow)
                return OperationResult<PredictionDto>.Failure("Prediction deadline has passed");

            var prediction = new Prediction
            {
                MatchId = request.MatchId,
                UserId = userId,
                PredictedHomeScore = request.Prediction.PredictedHomeScore,
                PredictedAwayScore = request.Prediction.PredictedAwayScore,
                SubmittedAt = DateTime.UtcNow
            };

            prediction.PredictedWinnerId = request.Prediction.PredictedHomeScore > request.Prediction.PredictedAwayScore
                ? match.HomeCompetitorId
                : request.Prediction.PredictedAwayScore > request.Prediction.PredictedHomeScore
                    ? match.AwayCompetitorId
                    : (int?)null;

            await _predictionRepository.AddAsync(prediction);

            var dto = _mapper.Map<PredictionDto>(prediction);
            return OperationResult<PredictionDto>.Success(dto);
        }
    }
}