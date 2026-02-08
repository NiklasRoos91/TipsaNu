using TipsaNu.Domain.Entities;

namespace TipsaNu.Domain.Interfaces
{
    public interface IPredictionRepository
    {
        Task<List<Prediction>> GetPredictionsForUserWithMatchAsync(int userId, CancellationToken cancellationToken = default);
        Task<Prediction?> GetPredictionForUserAndMatchAsync(int userId, int matchId, CancellationToken cancellationToken = default);
        Task<List<Prediction>> GetPredictionsForMatchAsync(int matchId, CancellationToken cancellationToken = default);
        Task UpdateRangeAsync(List<Prediction> predictions, CancellationToken cancellationToken = default);
    }
}
