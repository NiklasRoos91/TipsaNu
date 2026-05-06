using MediatR;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.AdminFeatures.AdminLeaderboards.Events.LeaderboardEntryUpdateRequested
{
    public class LeaderboardEntryUpdateRequestedEventHandler(ILeaderboardRepository leaderboardRepository)
        : INotificationHandler<LeaderboardEntryUpdateRequestedEvent>
    {
        public async Task Handle(LeaderboardEntryUpdateRequestedEvent notification, CancellationToken cancellationToken)
        {
            var totalPoints = await leaderboardRepository.GetTotalPointsForUserInTournamentAsync(
                notification.UserId,
                notification.TournamentId,
                cancellationToken
            );

            await leaderboardRepository.UpdateLeaderboardEntriesAsync(
                notification.UserId,
                notification.TournamentId,
                totalPoints,
                cancellationToken
            );
        }
    }
}