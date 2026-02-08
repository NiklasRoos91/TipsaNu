using MediatR;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Groups.DTOs;

namespace TipsaNu.Application.Features.Groups.Queries.GetGroupStandingsByGroupId
{
    public record GetGroupStandingsByGroupIdQuery(int GroupId)
        : IRequest<OperationResult<List<GroupStandingDto>>>;
}
