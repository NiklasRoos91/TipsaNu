using MediatR;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Matches.DTOs;
using TipsaNu.Application.Features.Matches.Mappers;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Enums;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.AdminFeatures.AdminMatches.Commands.UpdateMatchStatus;

public class UpdateMatchStatusCommandHandler(IGenericRepository<Match> matchRepository)
    : IRequestHandler<UpdateMatchStatusCommand, OperationResult<MatchDto>>
{
    public async Task<OperationResult<MatchDto>> Handle(UpdateMatchStatusCommand request, CancellationToken cancellationToken)
    {
        var match = await matchRepository.GetByIdAsync(request.MatchId, cancellationToken);
        if (match == null)
            return OperationResult<MatchDto>.Failure("Match not found");

        if (request.Status is MatchStatusEnum.Finished or MatchStatusEnum.InProgress 
            && match.StartTime > DateTime.Now)
        {
            return OperationResult<MatchDto>.Failure("You cannot start or finish a match that has not scheduled to start yet.");
        }

        match.Status = request.Status;

        await matchRepository.UpdateAsync(match, cancellationToken);

        return OperationResult<MatchDto>.Success(match.ToMatchDto());
    }
}
