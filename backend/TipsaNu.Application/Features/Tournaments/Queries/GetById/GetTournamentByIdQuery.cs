using MediatR;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Tournaments.DTOs;

namespace TipsaNu.Application.Features.Tournaments.Queries.GetById
{
    public record GetTournamentByIdQuery(int TournamentId) : IRequest<OperationResult<TournamentDto>>;
}
