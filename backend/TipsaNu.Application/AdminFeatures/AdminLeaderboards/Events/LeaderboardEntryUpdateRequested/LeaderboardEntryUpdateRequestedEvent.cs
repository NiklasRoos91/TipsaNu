using MediatR;

namespace TipsaNu.Application.AdminFeatures.AdminLeaderboards.Events.LeaderboardEntryUpdateRequested
{
    public class LeaderboardEntryUpdateRequestedEvent : INotification
    {
        public int TournamentId { get; }
        public int UserId { get; }

        public LeaderboardEntryUpdateRequestedEvent(int tournamentId, int userId)
        {
            TournamentId = tournamentId;
            UserId = userId;
        }
    }
}