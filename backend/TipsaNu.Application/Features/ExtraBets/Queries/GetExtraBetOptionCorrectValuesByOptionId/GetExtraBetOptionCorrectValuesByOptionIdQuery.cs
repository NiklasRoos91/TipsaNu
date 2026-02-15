using MediatR;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.ExtraBets.DTOs;

namespace TipsaNu.Application.Features.ExtraBets.Queries.GetExtraBetOptionCorrectValuesByOptionId
{
    public record GetExtraBetOptionCorrectValuesByOptionIdQuery(int OptionId)
        : IRequest<OperationResult<List<ExtraBetOptionCorrectValueDto>>>;
}
