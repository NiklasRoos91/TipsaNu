using MediatR;
using TipsaNu.Application.AdminFeatures.AdminExtraBets.DTOs;
using TipsaNu.Application.Commons.Results;

namespace TipsaNu.Application.AdminFeatures.AdminExtraBets.Commands.AddExtraBetOptionCorrectValues
{
    public record AddExtraBetOptionCorrectValuesCommand(int OptionId, SetExtraBetOptionCorrectValuesDto SetExtraBetOptionCorrectValuesDto) : IRequest<OperationResult<Unit>>;
}
