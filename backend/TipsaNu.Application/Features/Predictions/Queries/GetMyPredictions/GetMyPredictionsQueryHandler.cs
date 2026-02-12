using AutoMapper;
using MediatR;
using TipsaNu.Application.Commons.Interfaces;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Predictions.DTOs;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.Features.Predictions.Queries.GetMyPredictions
{
    public class GetMyPredictionsQueryHandler : IRequestHandler<GetMyPredictionsQuery, OperationResult<List<MatchPredictionDto>>>
    {
        private readonly IPredictionRepository _predictionRepository;
        private readonly ICurrentUserService _currentUser;
        private readonly IMapper _mapper;

        public GetMyPredictionsQueryHandler(
            IPredictionRepository predictionRepository,
            ICurrentUserService currentUser,
            IMapper mapper)
        {
            _predictionRepository = predictionRepository;
            _currentUser = currentUser;
            _mapper = mapper;
        }

        public async Task<OperationResult<List<MatchPredictionDto>>> Handle(GetMyPredictionsQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId;
            if (userId <= 0)
                return OperationResult<List<MatchPredictionDto>>.Failure("Unauthorized");

            var predictions = await _predictionRepository
                .GetPredictionsForUserWithMatchAsync(userId, cancellationToken);

            predictions.ForEach(p => p.Match = p.Match);

            var dtos = _mapper.Map<List<MatchPredictionDto>>(predictions);

            return OperationResult<List<MatchPredictionDto>>.Success(dtos);
        }
    }
}
