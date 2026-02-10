using MediatR;
using TipsaNu.Application.AdminFeatures.AdminExtraBets.DTOs;
using TipsaNu.Application.Commons.Results;

namespace TipsaNu.Application.AdminFeatures.AdminExtraBets.Commands.CreateExtraBetOptionCorrectValues
{
    public record CreateExtraBetOptionCorrectValuesCommand(int OptionId, SetExtraBetOptionCorrectValuesDto SetExtraBetOptionCorrectValuesDto) : IRequest<OperationResult<bool>>;
}
