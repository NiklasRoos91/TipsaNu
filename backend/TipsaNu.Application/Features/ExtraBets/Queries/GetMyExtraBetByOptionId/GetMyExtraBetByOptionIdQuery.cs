using MediatR;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.ExtraBets.DTOs;

namespace TipsaNu.Application.Features.ExtraBets.Queries.GetMyExtraBetByOptionId
{
    public record GetMyExtraBetByOptionIdQuery(int OptionId)
        : IRequest<OperationResult<ExtraBetDto>>;
}
