using MediatR;
using TipsaNu.Application.AdminFeatures.AdminLeaderboards.Events.LeaderboardEntryUpdateRequested;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.AdminFeatures.AdminExtraBets.Events
{
    public class ExtraBetOptionCorrectValuesUpdatedEventHandler
    : INotificationHandler<ExtraBetOptionCorrectValuesUpdatedEvent>
    {
        private readonly IExtraBetRepository _extraBetRepository;
        private readonly IMediator _mediator;

        public ExtraBetOptionCorrectValuesUpdatedEventHandler(IExtraBetRepository extraBetRepository, IMediator mediator)
        {
            _extraBetRepository = extraBetRepository;
            _mediator = mediator;
        }

        public async Task Handle(ExtraBetOptionCorrectValuesUpdatedEvent notification, CancellationToken cancellationToken)
        {
            var bets = await _extraBetRepository.GetBetsByOptionIdAsync(notification.OptionId, cancellationToken);

            var correctValues = await _extraBetRepository.GetCorrectValuesByOptionIdAsync(notification.OptionId, cancellationToken);

            foreach (var bet in bets)
            {
                bet.PointsAwarded = correctValues
                    .Any(cv => string.Equals(cv.Value.Trim(), bet.Value.Trim(), StringComparison.OrdinalIgnoreCase))
                    ? bet.ExtraBetOption.Points
                    : 0;

                await _mediator.Publish(
                    new LeaderboardEntryUpdateRequestedEvent(bet.ExtraBetOption.TournamentId, bet.UserId),
                    cancellationToken
                    );
            }

            await _extraBetRepository.UpdateRangeAsync(bets, cancellationToken);
        }
    }
}