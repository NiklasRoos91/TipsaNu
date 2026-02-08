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
    }
}
