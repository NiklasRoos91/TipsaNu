using AutoMapper;
using MediatR;
using TipsaNu.Application.Commons.Interfaces;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Leagues.DTOs;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.Features.Leagues.Queries.GetLeagueDetails
{
    public class GetLeagueDetailsQueryHandler
        : IRequestHandler<GetLeagueDetailsQuery, OperationResult<LeagueWithLeaderboardDto>>
    {
        private readonly ILeagueRepository _leagueRepository;
        private readonly ICurrentUserService _currentUser;
        private readonly IMapper _mapper;

        public GetLeagueDetailsQueryHandler(ILeagueRepository leagueRepository, ICurrentUserService currentUser, IMapper mapper)
        {
            _leagueRepository = leagueRepository;
            _currentUser = currentUser;
            _mapper = mapper;
        }

        public async Task<OperationResult<LeagueWithLeaderboardDto>> Handle(
            GetLeagueDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId;
            if (userId <= 0)
                return OperationResult<LeagueWithLeaderboardDto>.Failure("Unauthorized");

            var league = await _leagueRepository
                .GetLeagueWithMembersAndLeaderboardAsync(request.LeagueId, cancellationToken);

            if (league == null)
                return OperationResult<LeagueWithLeaderboardDto>.Failure("League not found");

            var isMemberOrAdmin = league.Members.Any(m => m.UserId == userId) || league.AdminUserId == userId;
            if (!isMemberOrAdmin)
                return OperationResult<LeagueWithLeaderboardDto>.Failure("You are not a member of this league");

            var dto = _mapper.Map<LeagueWithLeaderboardDto>(league);

            return OperationResult<LeagueWithLeaderboardDto>.Success(dto);
        }
    }
}