using MediatR;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Matches.DTOs;

namespace TipsaNu.Application.Features.Matches.Queries.GetMatchById
{
    public record GetMatchByIdQuery(int MatchId)
        : IRequest<OperationResult<MatchDto>>;
}
