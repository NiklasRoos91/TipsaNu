using MediatR;
using TipsaNu.Application.Commons.Interfaces;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Leagues.DTOs;
using TipsaNu.Application.Features.Leagues.Mappers;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.Features.Leagues.Queries.GetMyLeaguesInTournament
{
    public class GetMyLeaguesInTournamentQueryHandler(
        ILeagueRepository leagueRepository,
        IGenericRepository<Tournament> genericTournamentRepository,
        ICurrentUserService currentUser)
        : IRequestHandler<GetMyLeaguesInTournamentQuery, OperationResult<List<LeagueDto>>>
    {
        public async Task<OperationResult<List<LeagueDto>>> Handle(
            GetMyLeaguesInTournamentQuery request,
            CancellationToken cancellationToken)
        {
            var userId = currentUser.UserId;
            if (userId <= 0)
                return OperationResult<List<LeagueDto>>.Failure("Unauthorized");

            var tournament = await genericTournamentRepository.GetByIdAsync(request.TournamentId, cancellationToken);
            if (tournament == null)
                return OperationResult<List<LeagueDto>>.Failure("Tournament not found");

            var leagues = await leagueRepository.GetLeaguesForUserInTournamentAsync(request.TournamentId, userId, cancellationToken);

            return OperationResult<List<LeagueDto>>.Success(
                leagues.Select(l => l.ToLeagueDto()).ToList());      
        }
    }
}