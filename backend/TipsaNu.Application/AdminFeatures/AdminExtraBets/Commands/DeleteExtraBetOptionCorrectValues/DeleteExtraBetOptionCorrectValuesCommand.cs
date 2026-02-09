using MediatR;
using TipsaNu.Application.Commons.Results;

namespace TipsaNu.Application.AdminFeatures.AdminExtraBets.Commands.DeleteExtraBetOptionCorrectValues
{
    public record DeleteExtraBetOptionCorrectValuesCommand(int OptionId) : IRequest<OperationResult<bool>>;
}
