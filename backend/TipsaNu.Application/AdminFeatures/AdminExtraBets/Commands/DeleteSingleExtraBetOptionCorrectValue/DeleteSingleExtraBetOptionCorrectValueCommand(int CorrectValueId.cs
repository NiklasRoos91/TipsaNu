using MediatR;
using TipsaNu.Application.Commons.Results;

namespace TipsaNu.Application.AdminFeatures.AdminExtraBets.Commands.DeleteSingleExtraBetOptionCorrectValue
{
    public record DeleteSingleExtraBetOptionCorrectValueCommand(int CorrectValueId) : IRequest<OperationResult<bool>>;
}
