using MediatR;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Leagues.DTOs;

namespace TipsaNu.Application.Features.Leagues.Queries.GetMyLeaguesInTournament
{
    public record GetMyLeaguesInTournamentQuery(int TournamentId)
        : IRequest<OperationResult<List<LeagueDto>>>;
}
