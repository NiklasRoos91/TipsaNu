using TipsaNu.Domain.Entities;

namespace TipsaNu.Application.Services.Interfaces
{
    public interface IPointsCalculatorService
    {
        int CalculatePoints(Prediction prediction, Match match, List<PointRule> pointRules);
    }
}
