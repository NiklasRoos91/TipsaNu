using MediatR;

namespace TipsaNu.Application.AdminFeatures.AdminLeaderboards.Events.LeaderboardEntryUpdateRequested
{
    public class LeaderboardEntryUpdateRequestedEvent(int tournamentId, int userId) : INotification
    {
        public int TournamentId { get; } = tournamentId;
        public int UserId { get; } = userId;
    }
}