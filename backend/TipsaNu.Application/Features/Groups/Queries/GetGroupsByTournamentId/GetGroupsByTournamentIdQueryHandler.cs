using System.Reflection.Metadata;
using MediatR;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Groups.DTOs;
using TipsaNu.Application.Features.Groups.Mappers;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.Features.Groups.Queries.GetGroupsByTournamentID
{
    public class GetGroupsByTournamentHandler(IGroupRepository groupRepository)
        : IRequestHandler<GetGroupsByTournamentIdQuery, OperationResult<List<GroupDto>>>
    {
        public async Task<OperationResult<List<GroupDto>>> Handle(GetGroupsByTournamentIdQuery request, CancellationToken cancellationToken)
        {
            var groups = await groupRepository.GetGroupsByTournamentIdAsync(request.TournamentId, cancellationToken);

            if (!groups.Any())
                return OperationResult<List<GroupDto>>.Failure("No groups found for this tournament");

            return OperationResult<List<GroupDto>>.Success(
                groups.Select(g => g.ToDto()).ToList());
        }
    }
}
