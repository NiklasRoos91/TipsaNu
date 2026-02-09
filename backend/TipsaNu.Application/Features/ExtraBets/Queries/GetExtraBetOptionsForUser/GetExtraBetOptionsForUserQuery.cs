using MediatR;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.ExtraBets.DTOs;

namespace TipsaNu.Application.Features.ExtraBets.Queries.GetExtraBetOptionsForUser
{
    public record GetExtraBetOptionsForUserQuery(int TournamentId)
        : IRequest<OperationResult<List<ExtraBetOptionForUserDto>>>;
}
