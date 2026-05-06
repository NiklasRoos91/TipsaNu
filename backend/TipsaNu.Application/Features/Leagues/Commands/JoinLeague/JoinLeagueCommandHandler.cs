using MediatR;
using TipsaNu.Application.Commons.Interfaces;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Leagues.DTOs;
using TipsaNu.Application.Services.Interfaces;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.Features.Leagues.Commands.JoinLeague
{
    public class JoinLeagueCommandHandler(
        ILeagueRepository leagueRepository,
        ILeagueMemberService leagueMemberService,
        ICurrentUserService currentUser)
        : IRequestHandler<JoinLeagueCommand, OperationResult<LeagueMemberDto>>
    {
        public async Task<OperationResult<LeagueMemberDto>> Handle(JoinLeagueCommand request, CancellationToken cancellationToken)
        {
            var userId = currentUser.UserId;
            if (userId <= 0)
                return OperationResult<LeagueMemberDto>.Failure("Unauthorized");

            var league = await leagueRepository.GetLeagueByTournamentIdAndInvitationCodeAsync(
               request.TournamentId,
               request.Dto.InvitationCode,
               cancellationToken
           );
            if (league == null)
                return OperationResult<LeagueMemberDto>.Failure("Invalid invitation code or league not found");


            var alreadyMember = await leagueRepository.IsUserMemberAsync(league.LeagueId, userId, cancellationToken);
            if (alreadyMember)
                return OperationResult<LeagueMemberDto>.Failure("You are already a member of this league");

            if (league.MaxMembers > 0)
            {
                var count = await leagueRepository.GetMemberCountAsync(league.LeagueId, cancellationToken);
                if (count >= league.MaxMembers)
                    return OperationResult<LeagueMemberDto>.Failure("League is full");
            }

            var dto = await leagueMemberService.AddMemberWithLeaderboardAsync(league.LeagueId, userId, cancellationToken);

            return OperationResult<LeagueMemberDto>.Success(dto);
        }
    }
}