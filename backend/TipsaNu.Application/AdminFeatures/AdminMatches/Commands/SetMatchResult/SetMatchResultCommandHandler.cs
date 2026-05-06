using MediatR;
using TipsaNu.Application.AdminFeatures.AdminMatches.Events.MatchResultUpdated;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Matches.DTOs;
using TipsaNu.Application.Features.Matches.Mappers;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Enums;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.AdminFeatures.AdminMatches.Commands.SetMatchResult
{
    public class SetMatchResultCommandHandler(
        IGenericRepository<Match> matchRepository,
        IMediator mediator)
        : IRequestHandler<SetMatchResultCommand, OperationResult<MatchDto>>
    {
        public async Task<OperationResult<MatchDto>> Handle(SetMatchResultCommand request, CancellationToken cancellationToken)
        {
            var match = await matchRepository.GetByIdAsync(request.MatchId, cancellationToken);
            if (match == null)
                return OperationResult<MatchDto>.Failure("Match not found");

            // TODO: Only allow setting the result if the match has started or is finished.
            // Currently, any match can have its score set. Later we can add a check using match.StartTime 
            // or match.Status to prevent updating scores for matches that haven't been played yet.

            if (request.Dto.ScoreHome.HasValue)
                match.ScoreHome = request.Dto.ScoreHome;

            if (request.Dto.ScoreAway.HasValue)
                match.ScoreAway = request.Dto.ScoreAway;

            if (match is { ScoreHome: not null, ScoreAway: not null })
            {
                if (match.ScoreHome > match.ScoreAway)
                    match.WinnerCompetitorId = match.HomeCompetitorId;
                else if (match.ScoreAway > match.ScoreHome)
                    match.WinnerCompetitorId = match.AwayCompetitorId;
                else
                    match.WinnerCompetitorId = null;
            }

            match.Status = MatchStatusEnum.Finished;

            await matchRepository.UpdateAsync(match, cancellationToken);

            await mediator.Publish(new MatchResultUpdatedEvent(match.MatchId), cancellationToken);

            return OperationResult<MatchDto>.Success(match.ToMatchDto());
        }
    }
}
