using MediatR;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Matches.DTOs;
using TipsaNu.Domain.Enums;

namespace TipsaNu.Application.AdminFeatures.AdminMatches.Commands.UpdateMatchStatus
{
    public record UpdateMatchStatusCommand(int MatchId, MatchStatusEnum Status) : IRequest<OperationResult<MatchDto>>;
}