using TipsaNu.Application.Services.Interfaces;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Enums;

namespace TipsaNu.Application.Services.Implementations
{
    public class PointsCalculatorService : IPointsCalculatorService
    {
        public int CalculatePoints(Prediction prediction, Match match, List<PointRule> pointRules)
        {
            int points = 0;

            int matchOutcome = GetOutcome(match.ScoreHome.Value, match.ScoreAway.Value);
            int predictedOutcome = GetOutcome(prediction.PredictedHomeScore, prediction.PredictedAwayScore);

            foreach (var rule in pointRules)
            {
                switch (rule.Criterion)
                {
                    case TippingCriterionEnum.CorrectOutcome:
                        if (predictedOutcome == matchOutcome)
                            points += rule.Points;
                        break;

                    case TippingCriterionEnum.ExactScore:
                        if (prediction.PredictedHomeScore == match.ScoreHome.Value &&
                            prediction.PredictedAwayScore == match.ScoreAway.Value)
                            points += rule.Points;
                        break;

                    case TippingCriterionEnum.GoalDifference:
                        var predictedDiff = prediction.PredictedHomeScore - prediction.PredictedAwayScore;
                        var actualDiff = match.ScoreHome.Value - match.ScoreAway.Value;
                        if (predictedDiff == actualDiff)
                            points += rule.Points;
                        break;

                    case TippingCriterionEnum.Other:
                        // framtida regler
                        break;
                }
            }

            return points;
        }

        private int GetOutcome(int homeScore, int awayScore)
        {
            if (homeScore > awayScore) return 1;   // home wins
            if (awayScore > homeScore) return 2;   // away wins
            return 0;                              // draw
        }
    }
}