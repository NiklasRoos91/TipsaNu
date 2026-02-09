using Microsoft.EntityFrameworkCore;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Interfaces;
using TipsaNu.Infrastructure.Presistence;

namespace TipsaNu.Infrastructure.Repositories
{
    public class ExtraBetRepository : IExtraBetRepository
    {
        private readonly AppDbContext _context;

        public ExtraBetRepository(AppDbContext context)
        {
            _context = context;
        }

        // This method adds a new extra bet option to the database. It takes an ExtraBetOption entity and a cancellation token as parameters,
        public async Task<ExtraBetOption> AddExtraBetOptionAsync(ExtraBetOption option, CancellationToken cancellationToken = default)
        {
            await _context.ExtraBetOptions.AddAsync(option, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return option;
        }

        // This method adds a new choice to an existing extra bet option. It takes the option ID, the value of the choice, and a cancellation token
        // as parameters. It creates a new ExtraBetOptionChoice entity, adds it to the database context, saves the changes, and returns the created choice.
        public async Task<ExtraBetOptionChoice> AddExtraBetOptionChoiceAsync(int optionId, string value, CancellationToken cancellationToken = default)
        {
            var choice = new ExtraBetOptionChoice
            {
                OptionId = optionId,
                Value = value
            };
            await _context.ExtraBetOptionChoices.AddAsync(choice, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return choice;
        }

        // This method retrieves all extra bet options for a given tournament, including the choices for each option and any bets placed by the specified user.
        public async Task<List<ExtraBetOption>> GetOptionsWithUserBetAsync(int tournamentId, int userId, CancellationToken cancellationToken = default)
        {
            return await _context.ExtraBetOptions
                .AsNoTracking()
                .Where(o => o.TournamentId == tournamentId)
                .Include(o => o.ExtraBetOptionChoices)
                .Include(o => o.ExtraBets.Where(b => b.UserId == userId))
                .ToListAsync(cancellationToken);
        }

        // This method retrieves a specific extra bet option by its ID, including all associated choices.
        public async Task<ExtraBetOption?> GetOptionByIdWithChoicesAsync(
            int optionId,
            CancellationToken cancellationToken = default)
        {
            return await _context.ExtraBetOptions
                .AsNoTracking()
                .Include(o => o.ExtraBetOptionChoices)
                .FirstOrDefaultAsync(o => o.OptionId == optionId, cancellationToken);
        }

        // This method checks if a user has already placed a bet on a specific extra bet option.
        // It takes the user ID, option ID, and a cancellation token as parameters,
        public async Task<bool> UserHasBetOnOptionAsync(
            int userId,
            int optionId,
            CancellationToken cancellationToken = default)
        {
            return await _context.ExtraBets
                .AsNoTracking()
                .AnyAsync(b => b.UserId == userId && b.OptionId == optionId, cancellationToken);
        }
    }
}