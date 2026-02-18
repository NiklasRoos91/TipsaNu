namespace TipsaNu.Domain.Interfaces
{
    public interface ILeaderboardRepository
    {
        Task<int> GetTotalPointsForUserInTournamentAsync(int userId, int tournamentId, CancellationToken cancellationToken);

        Task UpdateLeaderboardEntriesAsync(int userId, int tournamentId, int totalPoints, CancellationToken cancellationToken);
    }
}