using MediatR;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.ExtraBets.DTOs;
using TipsaNu.Application.Features.ExtraBets.Mappers;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.Features.ExtraBets.Queries.GetExtraBetOptionCorrectValuesByOptionId
{
    public class GetExtraBetOptionCorrectValuesByOptionIdQueryHandler(
        IExtraBetRepository extraBetRepository)
        : IRequestHandler<GetExtraBetOptionCorrectValuesByOptionIdQuery,
            OperationResult<List<ExtraBetOptionCorrectValueDto>>>
    {
        public async Task<OperationResult<List<ExtraBetOptionCorrectValueDto>>> Handle(
            GetExtraBetOptionCorrectValuesByOptionIdQuery request,
            CancellationToken cancellationToken)
        {
            var entities = await extraBetRepository.GetCorrectValuesByOptionIdAsync(request.OptionId, cancellationToken);
            
            return OperationResult<List<ExtraBetOptionCorrectValueDto>>.Success(
                entities.Select(e => e.ToDto()).ToList());
        }
    }
}
