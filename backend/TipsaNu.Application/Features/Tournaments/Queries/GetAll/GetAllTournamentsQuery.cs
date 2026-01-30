using MediatR;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Tournaments.DTOs;

namespace TipsaNu.Application.Features.Tournaments.Queries.GetAll
{
    public record GetAllTournamentsQuery() : IRequest<OperationResult<List<TournamentDto>>>;

}
