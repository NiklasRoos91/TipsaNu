using MediatR;
using TipsaNu.Application.AdminFeatures.AdminMatches.DTOs;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Matches.DTOs;

namespace TipsaNu.Application.AdminFeatures.AdminMatches.Commands.SetMatchResult
{
    public record SetMatchResultCommand(int MatchId, SetMatchResultDto Dto)
        : IRequest<OperationResult<MatchDto>>;
}
