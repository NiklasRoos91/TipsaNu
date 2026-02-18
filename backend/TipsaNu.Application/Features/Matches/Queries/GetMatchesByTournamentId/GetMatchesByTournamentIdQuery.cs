using MediatR;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Matches.DTOs;

namespace TipsaNu.Application.Features.Matches.Queries.GetMatchesByTournamentId
{
    public record GetMatchesByTournamentIdQuery(int TournamentId)
        : IRequest<OperationResult<List<MatchDto>>>;
}
