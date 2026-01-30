using AutoMapper;
using MediatR;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Tournaments.DTOs;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.Features.Tournaments.Queries.GetAll
{
    public class GetAllTournamentsHandler : IRequestHandler<GetAllTournamentsQuery, OperationResult<List<TournamentDto>>>
    {
        private readonly IGenericRepository<Tournament> _tournamentRepository;
        private readonly IMapper _mapper;

        public GetAllTournamentsHandler(IGenericRepository<Tournament> tournamentRepository, IMapper mapper)
        {
            _tournamentRepository = tournamentRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<List<TournamentDto>>> Handle(GetAllTournamentsQuery request, CancellationToken cancellationToken)
        {
            var tournaments = await _tournamentRepository.GetAllAsync();

            var dtoList = _mapper.Map<List<TournamentDto>>(tournaments);

            dtoList = dtoList.OrderBy(t => t.StartsAt).ToList();

            return OperationResult<List<TournamentDto>>.Success(dtoList);
        }
    }
}