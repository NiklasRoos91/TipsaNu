using TipsaNu.Domain.Entities;

namespace TipsaNu.Domain.Interfaces
{
    public interface IExtraBetRepository
    {
        Task<ExtraBetOption> AddExtraBetOptionAsync(ExtraBetOption option, CancellationToken cancellationToken);
        Task<ExtraBetOptionChoice> AddExtraBetOptionChoiceAsync(int optionId, string value, CancellationToken cancellationToken);
    }
}
