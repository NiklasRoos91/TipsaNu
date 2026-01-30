using AutoMapper;
using MediatR;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Groups.DTOs;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.Features.Groups.Queries
{
    public class GetGroupsByTournamentHandler : IRequestHandler<GetGroupsByTournamentQuery, OperationResult<List<GroupDto>>>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;

        public GetGroupsByTournamentHandler(IGroupRepository groupRepository, IMapper mapper)
        {
            _groupRepository = groupRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<List<GroupDto>>> Handle(GetGroupsByTournamentQuery request, CancellationToken cancellationToken)
        {
            var groups = await _groupRepository.GetGroupsByTournamentIdAsync(request.TournamentId);

            if (!groups.Any())
                return OperationResult<List<GroupDto>>.Failure("No groups found for this tournament");

            var dtoList = _mapper.Map<List<GroupDto>>(groups);
            return OperationResult<List<GroupDto>>.Success(dtoList);
        }
    }
}
