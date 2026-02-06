using AutoMapper;
using MediatR;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Groups.DTOs;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.Features.Groups.Queries.GetGroupStandingsByGroupId
{
    public class GetGroupStandingsByGroupIdHandler
        : IRequestHandler<GetGroupStandingsByGroupIdQuery, OperationResult<List<GroupStandingDto>>>
    {
        private readonly IGenericRepository<Group> _groupRepository;
        private readonly IGroupStandingRepository _groupStandingRepository;
        private readonly IMapper _mapper;

        public GetGroupStandingsByGroupIdHandler(IGenericRepository<Group> groupRepository, IGroupStandingRepository groupStandingRepository, IMapper mapper)
        {
            _groupRepository = groupRepository;
            _groupStandingRepository = groupStandingRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<List<GroupStandingDto>>> Handle(
            GetGroupStandingsByGroupIdQuery request,
            CancellationToken cancellationToken)
        {
            var group = await _groupRepository.GetByIdAsync(request.GroupId);
            if (group == null)
                return OperationResult<List<GroupStandingDto>>.Failure("Group not found");

            var standings = await _groupStandingRepository.GetGroupStandingsByGroupIdAsync(request.GroupId);
            if (!standings.Any())
                return OperationResult<List<GroupStandingDto>>.Failure("No standings found for this group");

            var dtoList = _mapper.Map<List<GroupStandingDto>>(standings);
            return OperationResult<List<GroupStandingDto>>.Success(dtoList);
        }
    }
}