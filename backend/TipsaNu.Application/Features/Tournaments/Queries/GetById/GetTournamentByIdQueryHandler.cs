using MediatR;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Tournaments.DTOs;
using TipsaNu.Application.Features.Tournaments.Mappers;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.Features.Tournaments.Queries.GetById
{
    public class GetTournamentByIdHandler(IGenericRepository<Tournament> tournamentRepository)
        : IRequestHandler<GetTournamentByIdQuery, OperationResult<TournamentDto>>
    {
        public async Task<OperationResult<TournamentDto>> Handle(GetTournamentByIdQuery request, CancellationToken cancellationToken)
        {
            var tournament = await tournamentRepository.GetByIdAsync(request.TournamentId, cancellationToken);

            if (tournament == null)
                return OperationResult<TournamentDto>.Failure("Tournament not found");

            return OperationResult<TournamentDto>.Success(tournament.ToTournamentDto());
        }
    }
}