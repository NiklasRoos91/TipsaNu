using MediatR;

namespace TipsaNu.Application.AdminFeatures.AdminMatches.Events.MatchResultUpdated
{
    public record MatchResultUpdatedEvent(int MatchId) : INotification;
}
