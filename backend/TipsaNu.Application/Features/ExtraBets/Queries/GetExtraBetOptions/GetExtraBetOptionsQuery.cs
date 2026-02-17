using MediatR;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.ExtraBets.DTOs;

namespace TipsaNu.Application.Features.ExtraBets.Queries.GetExtraBetOptions
{
    public record GetExtraBetOptionsQuery(int TournamentId, string Status)
        : IRequest<OperationResult<List<ExtraBetOptionDto>>>;

}
