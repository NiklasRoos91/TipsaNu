using AutoMapper;
using MediatR;
using TipsaNu.Application.Commons.Interfaces;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Leagues.DTOs;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.Features.Leagues.Queries.GetMyLeaguesInTournament
{
    public class GetMyLeaguesInTournamentQueryHandler
            : IRequestHandler<GetMyLeaguesInTournamentQuery, OperationResult<List<LeagueDto>>>
    {
        private readonly ILeagueRepository _leagueRepository;
        private readonly IGenericRepository<Tournament> _genericTournamentRepository;
        private readonly ICurrentUserService _currentUser;
        private readonly IMapper _mapper;

        public GetMyLeaguesInTournamentQueryHandler(
            ILeagueRepository leagueRepository,
            IGenericRepository<Tournament> genericTournamentRepository,
            ICurrentUserService currentUser,
            IMapper mapper)
        {
            _leagueRepository = leagueRepository;
            _genericTournamentRepository = genericTournamentRepository;
            _currentUser = currentUser;
            _mapper = mapper;
        }

        public async Task<OperationResult<List<LeagueDto>>> Handle(
            GetMyLeaguesInTournamentQuery request,
            CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId;
            if (userId <= 0)
                return OperationResult<List<LeagueDto>>.Failure("Unauthorized");

            var tournament = await _genericTournamentRepository.GetByIdAsync(request.TournamentId);
            if (tournament == null)
                return OperationResult<List<LeagueDto>>.Failure("Tournament not found");

            var leagues = await _leagueRepository.GetLeaguesForUserInTournamentAsync(request.TournamentId, userId, cancellationToken);

            var dto = _mapper.Map<List<LeagueDto>>(leagues);
            return OperationResult<List<LeagueDto>>.Success(dto);
        }
    }
}