using MediatR;

namespace TipsaNu.Application.AdminFeatures.AdminExtraBets.Events
{
    public record ExtraBetOptionCorrectValuesUpdatedEvent(int OptionId) : INotification;
}