using TipsaNu.Domain.Entities;

namespace TipsaNu.Domain.Interfaces
{
    public interface IExtraBetRepository
    {
        Task<ExtraBetOption> AddExtraBetOptionAsync(ExtraBetOption option, CancellationToken cancellationToken = default);
        Task<ExtraBetOptionChoice> AddExtraBetOptionChoiceAsync(int optionId, string value, CancellationToken cancellationToken = default);
        Task<List<ExtraBetOption>> GetOptionsWithUserBetAsync(int tournamentId, int userId, CancellationToken cancellationToken = default);
        Task<ExtraBetOption?> GetOptionByIdWithChoicesAsync(int optionId, CancellationToken cancellationToken = default);
        Task<bool> UserHasBetOnOptionAsync(int userId, int optionId, CancellationToken cancellationToken = default);
    }
}
