using MediatR;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Groups.DTOs;
using TipsaNu.Application.Features.Groups.Mappers;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.Features.Groups.Queries.GetGroupStandingsByGroupId
{
    public class GetGroupStandingsByGroupIdHandler(
        IGenericRepository<Group> groupRepository,
        IGroupStandingRepository groupStandingRepository)
        : IRequestHandler<GetGroupStandingsByGroupIdQuery, OperationResult<List<GroupStandingDto>>>
    {
        public async Task<OperationResult<List<GroupStandingDto>>> Handle(
            GetGroupStandingsByGroupIdQuery request,
            CancellationToken cancellationToken)
        {
            var group = await groupRepository.GetByIdAsync(request.GroupId, cancellationToken);
            if (group == null)
                return OperationResult<List<GroupStandingDto>>.Failure("Group not found");

            var standings = await groupStandingRepository.GetGroupStandingsByGroupIdAsync(request.GroupId, cancellationToken);
            if (!standings.Any())
                return OperationResult<List<GroupStandingDto>>.Failure("No standings found for this group");

            return OperationResult<List<GroupStandingDto>>.Success(
                standings.Select(s => s.ToDto()).ToList());
        }
    }
}