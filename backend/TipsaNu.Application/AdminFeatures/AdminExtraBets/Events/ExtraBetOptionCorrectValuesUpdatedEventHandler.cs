using MediatR;
using TipsaNu.Application.AdminFeatures.AdminLeaderboards.Events.LeaderboardEntryUpdateRequested;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.AdminFeatures.AdminExtraBets.Events
{
    public class ExtraBetOptionCorrectValuesUpdatedEventHandler(
        IExtraBetRepository extraBetRepository,
        IMediator mediator) : INotificationHandler<ExtraBetOptionCorrectValuesUpdatedEvent>
    {
        public async Task Handle(ExtraBetOptionCorrectValuesUpdatedEvent notification, CancellationToken cancellationToken)
        {
            var bets = await extraBetRepository.GetBetsByOptionIdAsync(notification.OptionId, cancellationToken);
            var correctValues = await extraBetRepository.GetCorrectValuesByOptionIdAsync(notification.OptionId, cancellationToken);

            foreach (var bet in bets)
            {
                bet.PointsAwarded = correctValues
                    .Any(cv => string.Equals(cv.Value.Trim(), bet.Value.Trim(), StringComparison.OrdinalIgnoreCase))
                    ? bet.ExtraBetOption.Points
                    : 0;
            }

            await extraBetRepository.UpdateRangeAsync(bets, cancellationToken);
            
            var uniqueUserIds = bets.Select(b => b.UserId).Distinct();
            var tournamentId = bets.FirstOrDefault()?.ExtraBetOption?.TournamentId;

            if (tournamentId.HasValue)
            {
                foreach (var userId in uniqueUserIds)
                {
                    await mediator.Publish(
                        new LeaderboardEntryUpdateRequestedEvent(tournamentId.Value, userId),
                        cancellationToken
                    );
                }
            }
        }
    }
}