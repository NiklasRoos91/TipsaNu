using MediatR;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.AdminFeatures.AdminLeaderboards.Events.LeaderboardEntryUpdateRequested
{
    public class LeaderboardEntryUpdateRequestedEventHandler
        : INotificationHandler<LeaderboardEntryUpdateRequestedEvent>
    {
        private readonly ILeaderboardRepository _leaderboardRepository;

        public LeaderboardEntryUpdateRequestedEventHandler(ILeaderboardRepository leaderboardRepository)
        {
            _leaderboardRepository = leaderboardRepository;
        }

        public async Task Handle(LeaderboardEntryUpdateRequestedEvent notification, CancellationToken cancellationToken)
        {
            var totalPoints = await _leaderboardRepository.GetTotalPointsForUserInTournamentAsync(
                notification.UserId,
                notification.TournamentId,
                cancellationToken
            );

            await _leaderboardRepository.UpdateLeaderboardEntriesAsync(
                notification.UserId,
                notification.TournamentId,
                totalPoints,
                cancellationToken
            );
        }
    }
}