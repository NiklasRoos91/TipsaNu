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

        public async Task<ExtraBetOption> AddExtraBetOptionAsync(ExtraBetOption option, CancellationToken cancellationToken)
        {
            await _context.ExtraBetOptions.AddAsync(option, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return option;
        }

        public async Task<ExtraBetOptionChoice> AddExtraBetOptionChoiceAsync(int optionId, string value, CancellationToken cancellationToken)
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
    }
}