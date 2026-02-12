using Microsoft.EntityFrameworkCore;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Interfaces;
using TipsaNu.Infrastructure.Presistence;

namespace TipsaNu.Infrastructure.Repositories
{
    public class PredictionRepository : IPredictionRepository
    {
        private readonly AppDbContext _context;

        public PredictionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Prediction>> GetPredictionsForUserWithMatchAsync(int userId, CancellationToken cancellationToken = default)
        {
            return await _context.Predictions
                .Where(p => p.UserId == userId)
                .Include(p => p.Match)
                    .ThenInclude(m => m.HomeCompetitor)
                .Include(p => p.Match)
                    .ThenInclude(m => m.AwayCompetitor)
                .OrderByDescending(p => p.SubmittedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<Prediction?> GetPredictionForUserAndMatchAsync(int userId, int matchId, CancellationToken cancellationToken = default)
        {
            return await _context.Predictions
                .Where(p => p.UserId == userId && p.MatchId == matchId)
                .Include(p => p.Match)
                    .ThenInclude(m => m.HomeCompetitor)
                .Include(p => p.Match)
                    .ThenInclude(m => m.AwayCompetitor)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<Prediction>> GetPredictionsForMatchAsync(int matchId, CancellationToken cancellationToken = default)
        {
            return await _context.Predictions
                .Where(p => p.MatchId == matchId)
                .ToListAsync(cancellationToken);
        }

        public async Task UpdateRangeAsync(List<Prediction> predictions, CancellationToken cancellationToken = default)
        {
            _context.Predictions.UpdateRange(predictions);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Prediction> GetByUserAndMatchAsync(int userId, int matchId, CancellationToken cancellationToken = default)
        {
            return await _context.Predictions
                .Where(p => p.UserId == userId && p.MatchId == matchId)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
