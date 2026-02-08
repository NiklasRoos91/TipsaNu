using MediatR;
using TipsaNu.Application.Services.Interfaces;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.AdminFeatures.AdminMatches.Events.MatchResultUpdated
{
    public class MatchResultUpdatedEventHandler : INotificationHandler<MatchResultUpdatedEvent>
    {
        private readonly IMatchRepository _matchRepository;
        private readonly IPredictionRepository _predictionRepository;
        private readonly IPointRuleRepository _pointRuleRepository;
        private readonly IPointsCalculatorService _pointsCalculator;


        public MatchResultUpdatedEventHandler(
            IMatchRepository matchRepository,
            IPredictionRepository predictionRepository,
            IPointRuleRepository pointRuleRepository,
            IPointsCalculatorService pointsCalculatorService
            )
        {
            _matchRepository = matchRepository;
            _predictionRepository = predictionRepository;
            _pointRuleRepository = pointRuleRepository;
            _pointsCalculator = pointsCalculatorService;
        }

        public async Task Handle(MatchResultUpdatedEvent notification, CancellationToken cancellationToken)
        {
            var match = await _matchRepository.GetMatchWithTournamentAsync(notification.MatchId, cancellationToken);

            if (match == null)
                return;

            if (!match.ScoreHome.HasValue || !match.ScoreAway.HasValue)
                return;

            var pointRules = await _pointRuleRepository.GetPointRulesForTemplateAndMatchTypeAsync(
                match.Tournament.TemplateId,
                match.MatchType,
                cancellationToken);

            if (pointRules.Count == 0)
                return;

            var predictions = await _predictionRepository.GetPredictionsForMatchAsync(match.MatchId, cancellationToken);

            foreach (var prediction in predictions)
            {
                prediction.PointsAwarded = _pointsCalculator.CalculatePoints(prediction, match, pointRules);
            }

            await _predictionRepository.UpdateRangeAsync(predictions, cancellationToken);
        }
    }
}