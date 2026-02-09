using MediatR;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.AdminFeatures.AdminExtraBets.Events
{
    public class ExtraBetOptionCorrectValuesUpdatedEventHandler
    : INotificationHandler<ExtraBetOptionCorrectValuesUpdatedEvent>
    {
        private readonly IExtraBetRepository _extraBetRepository;

        public ExtraBetOptionCorrectValuesUpdatedEventHandler(IExtraBetRepository extraBetRepository)
        {
            _extraBetRepository = extraBetRepository;
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
            }

            await _extraBetRepository.UpdateRangeAsync(bets, cancellationToken);
        }
    }
}