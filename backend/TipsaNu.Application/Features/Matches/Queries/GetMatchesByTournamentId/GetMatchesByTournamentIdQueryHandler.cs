using AutoMapper;
using MediatR;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Matches.DTOs;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.Features.Matches.Queries.GetMatchesByTournamentId
{
    public class GetMatchesByTournamentIdHandler
            : IRequestHandler<GetMatchesByTournamentIdQuery, OperationResult<List<MatchDto>>>
    {
        private readonly IGenericRepository<Domain.Entities.Match> _matchRepository;
        private readonly IMapper _mapper;

        public GetMatchesByTournamentIdHandler(IGenericRepository<Domain.Entities.Match> matchRepository, IMapper mapper)
        {
            _matchRepository = matchRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<List<MatchDto>>> Handle(GetMatchesByTournamentIdQuery request, CancellationToken cancellationToken)
        {
            var matches = (await _matchRepository.GetAllAsync(
                cancellationToken,
                m => m.HomeCompetitor,
                m => m.AwayCompetitor
            ))
            .Where(m => m.TournamentId == request.TournamentId)
            .ToList();

            if (!matches.Any())
                return OperationResult<List<MatchDto>>.Failure("No matches found for this tournament");

            var dtoList = _mapper.Map<List<MatchDto>>(matches);

            dtoList = dtoList.OrderBy(m => m.StartTime).ToList();

            return OperationResult<List<MatchDto>>.Success(dtoList);
        }
    }
}
