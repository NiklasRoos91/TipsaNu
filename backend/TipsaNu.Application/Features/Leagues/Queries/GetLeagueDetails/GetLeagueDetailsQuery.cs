using MediatR;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Leagues.DTOs;

namespace TipsaNu.Application.Features.Leagues.Queries.GetLeagueDetails
{
    public record GetLeagueDetailsQuery(int LeagueId)
        : IRequest<OperationResult<LeagueWithLeaderboardDto>>;
}
