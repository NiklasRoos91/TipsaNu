using MediatR;
using TipsaNu.Application.AdminFeatures.AdminLeaderboards.Events.LeaderboardEntryUpdateRequested;
using TipsaNu.Application.Services.Interfaces;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.AdminFeatures.AdminMatches.Events.MatchResultUpdated
{
    public class MatchResultUpdatedEventHandler(
        IMatchRepository matchRepository,
        IPredictionRepository predictionRepository,
        IPointRuleRepository pointRuleRepository,
        IPointsCalculatorService pointsCalculatorService,
        IMediator mediator)
        : INotificationHandler<MatchResultUpdatedEvent>
    {
        public async Task Handle(MatchResultUpdatedEvent notification, CancellationToken cancellationToken)
        {
            var match = await matchRepository.GetMatchWithTournamentAsync(notification.MatchId, cancellationToken);
            if (match == null || !match.ScoreHome.HasValue || !match.ScoreAway.HasValue)
                return;

            var pointRules = await pointRuleRepository.GetPointRulesForTemplateAndMatchTypeAsync(
                match.Tournament.TemplateId,
                match.MatchType,
                cancellationToken);

            if (pointRules.Count == 0)
                return;

            var predictions = await predictionRepository.GetPredictionsForMatchAsync(match.MatchId, cancellationToken);

            foreach (var prediction in predictions)
            {
                prediction.PointsAwarded = pointsCalculatorService.CalculatePoints(prediction, match, pointRules);
            }

            await predictionRepository.UpdateRangeAsync(predictions, cancellationToken);
            
            var uniqueUserIds = predictions.Select(p => p.UserId).Distinct();

            foreach (var userId in uniqueUserIds)
            {
                await mediator.Publish(
                    new LeaderboardEntryUpdateRequestedEvent(match.TournamentId, userId),
                    cancellationToken
                );
            }
        }
    }
}