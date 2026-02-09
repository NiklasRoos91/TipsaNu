using MediatR;
using TipsaNu.Application.AdminFeatures.AdminExtraBets.DTOs;
using TipsaNu.Application.Commons.Results;

namespace TipsaNu.Application.AdminFeatures.AdminExtraBets.Commands.SetExtraBetOptionCorrectValues
{
    public record SetExtraBetOptionCorrectValuesCommand(int OptionId, SetExtraBetOptionCorrectValuesDto SetExtraBetOptionCorrectValuesDto) : IRequest<OperationResult<Unit>>;
}
