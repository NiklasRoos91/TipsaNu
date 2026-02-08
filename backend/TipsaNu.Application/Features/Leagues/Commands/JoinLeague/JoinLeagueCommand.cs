using MediatR;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Leagues.DTOs;

namespace TipsaNu.Application.Features.Leagues.Commands.JoinLeague
{
    public record JoinLeagueCommand(int TournamentId, JoinLeagueDto Dto)
        : IRequest<OperationResult<LeagueMemberDto>>;
}
