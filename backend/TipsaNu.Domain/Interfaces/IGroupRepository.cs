using TipsaNu.Domain.Entities;

namespace TipsaNu.Domain.Interfaces
{
    public interface IGroupRepository
    {
        Task<IEnumerable<Group>> GetGroupsByTournamentIdAsync(int tournamentId, CancellationToken cancellationToken = default);
    }
}