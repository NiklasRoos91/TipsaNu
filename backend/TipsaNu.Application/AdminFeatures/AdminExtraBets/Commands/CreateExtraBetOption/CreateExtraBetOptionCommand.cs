using MediatR;
using TipsaNu.Application.AdminFeatures.AdminExtraBets.DTOs;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.ExtraBets.DTOs;

namespace TipsaNu.Application.AdminFeatures.AdminExtraBets.Commands.CreateExtraBetOption
{
    public record CreateExtraBetOptionCommand(CreateExtraBetOptionDto Dto)
        : IRequest<OperationResult<ExtraBetOptionDto>>;
}
