using MediatR;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Groups.DTOs;

namespace TipsaNu.Application.Features.Groups.Queries.GetMatchesByGroupId
{
    public record GetMatchesByGroupIdQuery(int GroupId)
        : IRequest<OperationResult<List<MatchDto>>>;
}