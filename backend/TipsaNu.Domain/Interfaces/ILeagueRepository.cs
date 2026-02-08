using TipsaNu.Domain.Entities;

namespace TipsaNu.Domain.Interfaces
{
    public interface ILeagueRepository
    {
        Task<List<League>> GetByUserIdAsync(int userId, int tournamentId, CancellationToken cancellationToken = default);
        Task<bool> InvitationCodeExistsAsync(int tournamentId, string invitationCode, CancellationToken cancellationToken = default);
        Task<List<League>> GetLeaguesForUserInTournamentAsync(int tournamentId, int userId, CancellationToken cancellationToken = default);
        Task<League?> GetLeagueByTournamentIdAndInvitationCodeAsync(int tournamentId, string invitationCode, CancellationToken cancellationToken = default);
        Task<bool> IsUserMemberAsync(int leagueId, int userId, CancellationToken cancellationToken = default);
        Task<int> GetMemberCountAsync(int leagueId, CancellationToken cancellationToken = default);
        Task<LeagueMember> AddLeagueMemberAsync(LeagueMember member, CancellationToken cancellationToken = default);
        Task<LeaderboardEntry> AddLeaderboardEntryAsync(LeaderboardEntry entry, CancellationToken cancellationToken = default);
        Task<League?> GetLeagueWithMembersAndLeaderboardAsync(int leagueId, CancellationToken cancellationToken = default);
    }
}
