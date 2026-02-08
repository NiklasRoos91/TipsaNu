using MediatR;
using TipsaNu.Application.Commons.Interfaces;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Leagues.DTOs;
using TipsaNu.Application.Services.Interfaces;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.Features.Leagues.Commands.JoinLeague
{
    public class JoinLeagueCommandHandler : IRequestHandler<JoinLeagueCommand, OperationResult<LeagueMemberDto>>
    {
        private readonly ILeagueRepository _leagueRepository;
        private readonly ILeagueMemberService _leagueMemberService;
        private readonly ICurrentUserService _currentUser;

        public JoinLeagueCommandHandler(ILeagueRepository leagueRepository, ILeagueMemberService leagueMemberService, ICurrentUserService currentUser)
        {
            _leagueRepository = leagueRepository;
            _leagueMemberService = leagueMemberService;
            _currentUser = currentUser;
        }

        public async Task<OperationResult<LeagueMemberDto>> Handle(JoinLeagueCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId;
            if (userId <= 0)
                return OperationResult<LeagueMemberDto>.Failure("Unauthorized");

            var league = await _leagueRepository.GetLeagueByTournamentIdAndInvitationCodeAsync(
               request.TournamentId,
               request.Dto.InvitationCode,
               cancellationToken
           );
            if (league == null)
                return OperationResult<LeagueMemberDto>.Failure("Invalid invitation code or league not found");


            var alreadyMember = await _leagueRepository.IsUserMemberAsync(league.LeagueId, userId, cancellationToken);
            if (alreadyMember)
                return OperationResult<LeagueMemberDto>.Failure("You are already a member of this league");

            if (league.MaxMembers > 0)
            {
                var count = await _leagueRepository.GetMemberCountAsync(league.LeagueId, cancellationToken);
                if (count >= league.MaxMembers)
                    return OperationResult<LeagueMemberDto>.Failure("League is full");
            }

            var dto = await _leagueMemberService.AddMemberWithLeaderboardAsync(league.LeagueId, userId, cancellationToken);

            return OperationResult<LeagueMemberDto>.Success(dto);
        }
    }
}