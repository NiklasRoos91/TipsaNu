using AutoMapper;
using MediatR;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Matches.DTOs;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.Features.Groups.Queries.GetMatchesByGroupId
{
    public class GetMatchesByGroupIdHandler
        : IRequestHandler<GetMatchesByGroupIdQuery, OperationResult<List<MatchDto>>>
    {
        private readonly IMatchRepository _matchRepository;
        private readonly IMapper _mapper;

        public GetMatchesByGroupIdHandler(IMatchRepository matchRepository, IMapper mapper)
        {
            _matchRepository = matchRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<List<MatchDto>>> Handle(GetMatchesByGroupIdQuery request, CancellationToken cancellationToken)
        {
            var matches = await _matchRepository.GetMatchesByGroupIdAsync(request.GroupId, cancellationToken);

            if (!matches.Any())
                return OperationResult<List<MatchDto>>.Failure("No matches found for this group");

            var dtoList = _mapper.Map<List<MatchDto>>(matches);

            dtoList = dtoList.OrderBy(m => m.StartTime).ToList();

            return OperationResult<List<MatchDto>>.Success(dtoList);
        }
    }
}
