using MediatR;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Groups.DTOs;

namespace TipsaNu.Application.Features.Groups.Queries.GetGroupsByTournamentID
{
    public record GetGroupsByTournamentIdQuery(int TournamentId)
        : IRequest<OperationResult<List<GroupDto>>>;
}
