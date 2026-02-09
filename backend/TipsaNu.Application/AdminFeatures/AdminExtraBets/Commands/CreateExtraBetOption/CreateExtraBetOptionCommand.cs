using MediatR;
using TipsaNu.Application.AdminFeatures.AdminExtraBets.DTOs;
using TipsaNu.Application.Commons.Results;

namespace TipsaNu.Application.AdminFeatures.AdminExtraBets.Commands.CreateExtraBetOption
{
    public record CreateExtraBetOptionCommand(CreateExtraBetOptionDto Dto)
        : IRequest<OperationResult<ExtraBetOptionDto>>;
}
