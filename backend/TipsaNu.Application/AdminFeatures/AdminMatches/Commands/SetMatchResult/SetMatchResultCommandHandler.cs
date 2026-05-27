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

            if (match.Status != MatchStatusEnum.Finished)
            {
                return OperationResult<MatchDto>.Failure("Du måste markera matchen som avslutad innan du kan spara ett resultat.");
            }
            
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
            
            await matchRepository.UpdateAsync(match, cancellationToken);

            await mediator.Publish(new MatchResultUpdatedEvent(match.MatchId), cancellationToken);

            return OperationResult<MatchDto>.Success(match.ToMatchDto());
        }
    }
}
