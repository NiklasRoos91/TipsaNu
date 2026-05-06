using MediatR;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Matches.DTOs;
using TipsaNu.Application.Features.Matches.Mappers;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.Features.Groups.Queries.GetMatchesByGroupId
{
    public class GetMatchesByGroupIdHandler(IMatchRepository matchRepository)
        : IRequestHandler<GetMatchesByGroupIdQuery, OperationResult<List<MatchDto>>>
    {
        public async Task<OperationResult<List<MatchDto>>> Handle(GetMatchesByGroupIdQuery request, CancellationToken cancellationToken)
        {
            var matches = await matchRepository.GetMatchesByGroupIdAsync(request.GroupId, cancellationToken);

            if (matches.Count == 0)
                return OperationResult<List<MatchDto>>.Failure("No matches found for this group");

            var result = matches
                .Select(m => m.ToMatchDto())
                .OrderBy(m => m.StartTime)
                .ToList();

            return OperationResult<List<MatchDto>>.Success(result);
        }
    }
}
