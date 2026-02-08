using TipsaNu.Domain.Entities;

namespace TipsaNu.Domain.Interfaces
{
    public interface IPredictionRepository
    {
        Task<List<Prediction>> GetPredictionsForUserWithMatchAsync(int userId, CancellationToken cancellationToken = default);
    }
}
