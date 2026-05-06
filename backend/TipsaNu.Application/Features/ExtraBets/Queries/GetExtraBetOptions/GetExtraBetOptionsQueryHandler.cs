using MediatR;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.ExtraBets.DTOs;
using TipsaNu.Application.Features.ExtraBets.Mappers;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.Features.ExtraBets.Queries.GetExtraBetOptions
{
    public class GetExtraBetOptionsQueryHandler(
        IExtraBetRepository extraBetRepository)
        : IRequestHandler<GetExtraBetOptionsQuery, OperationResult<List<ExtraBetOptionDto>>>
    {
        public async Task<OperationResult<List<ExtraBetOptionDto>>> Handle(
            GetExtraBetOptionsQuery request,
            CancellationToken cancellationToken)
        {
            var entities = await extraBetRepository.GetExtraBetOptionsAsync(
                request.TournamentId,
                request.Status,
                cancellationToken);

            return OperationResult<List<ExtraBetOptionDto>>.Success(
                entities.Select(e => e.ToDto()).ToList());
        }
    }
}
