using MediatR;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Leagues.DTOs;

namespace TipsaNu.Application.Features.Leagues.Commands.CreateLeague
{
    public record CreateLeagueCommand(CreateLeagueDto LeagueDto)
        : IRequest<OperationResult<CreatedLeagueWithMemberDto>>;
}
