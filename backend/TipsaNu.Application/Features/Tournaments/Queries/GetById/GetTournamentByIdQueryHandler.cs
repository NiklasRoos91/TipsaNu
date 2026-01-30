using AutoMapper;
using MediatR;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Tournaments.DTOs;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.Features.Tournaments.Queries.GetById
{
    public class GetTournamentByIdHandler : IRequestHandler<GetTournamentByIdQuery, OperationResult<TournamentDto>>
    {
        private readonly IGenericRepository<Tournament> _tournamentRepository;
        private readonly IMapper _mapper;

        public GetTournamentByIdHandler(IGenericRepository<Tournament> tournamentRepository, IMapper mapper)
        {
            _tournamentRepository = tournamentRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<TournamentDto>> Handle(GetTournamentByIdQuery request, CancellationToken cancellationToken)
        {
            var tournament = await _tournamentRepository.GetByIdAsync(request.TournamentId);

            if (tournament == null)
                return OperationResult<TournamentDto>.Failure("Tournament not found");

            var dto = _mapper.Map<TournamentDto>(tournament);

            return OperationResult<TournamentDto>.Success(dto);
        }
    }
}