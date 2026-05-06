using MediatR;
using TipsaNu.Application.Commons.Interfaces;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Leagues.DTOs;
using TipsaNu.Application.Features.Leagues.Mappers;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.Features.Leagues.Queries.GetLeagueDetails
{
    public class GetLeagueDetailsQueryHandler(
        ILeagueRepository leagueRepository,
        ICurrentUserService currentUser)
        : IRequestHandler<GetLeagueDetailsQuery, OperationResult<LeagueWithLeaderboardDto>>
    {
        public async Task<OperationResult<LeagueWithLeaderboardDto>> Handle(
            GetLeagueDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var userId = currentUser.UserId;
            if (userId <= 0)
                return OperationResult<LeagueWithLeaderboardDto>.Failure("Unauthorized");

            var league = await leagueRepository
                .GetLeagueWithMembersAndLeaderboardAsync(request.LeagueId, cancellationToken);

            if (league == null)
                return OperationResult<LeagueWithLeaderboardDto>.Failure("League not found");

            var isMemberOrAdmin = league.Members.Any(m => m.UserId == userId) || league.AdminUserId == userId;
            if (!isMemberOrAdmin)
                return OperationResult<LeagueWithLeaderboardDto>.Failure("You are not a member of this league");

            return OperationResult<LeagueWithLeaderboardDto>.Success(league.ToLeagueWithLeaderboardDto());
        }
    }
}