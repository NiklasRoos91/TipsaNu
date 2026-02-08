using TipsaNu.Domain.Entities;

namespace TipsaNu.Domain.Interfaces
{
    public interface IMatchRepository
    {
        Task<List<Match>> GetMatchesByGroupIdAsync(int groupId, CancellationToken cancellationToken = default);
        Task<Match?> GetMatchWithCompetitorsAsync(int matchId, CancellationToken cancellationToken = default);
        Task<Match?> GetMatchWithTournamentAsync(int matchId, CancellationToken cancellationToken = default);
    }
}