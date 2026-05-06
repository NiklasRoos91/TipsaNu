using MediatR;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Tournaments.DTOs;
using TipsaNu.Application.Features.Tournaments.Mappers;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.Features.Tournaments.Queries.GetAll
{
    public class GetAllTournamentsHandler(IGenericRepository<Tournament> tournamentRepository)
        : IRequestHandler<GetAllTournamentsQuery, OperationResult<List<TournamentDto>>>
    {
        public async Task<OperationResult<List<TournamentDto>>> Handle(GetAllTournamentsQuery request, CancellationToken cancellationToken)
        {
            var tournaments = await tournamentRepository.GetAllAsync(cancellationToken);

            var dtoList = tournaments
                .Select(t => t.ToTournamentDto())
                .OrderBy(t => t.StartsAt)
                .ToList();

            return OperationResult<List<TournamentDto>>.Success(dtoList);
        }
    }
}