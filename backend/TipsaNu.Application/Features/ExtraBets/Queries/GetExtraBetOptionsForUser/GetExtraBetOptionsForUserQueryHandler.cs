using AutoMapper;
using MediatR;
using TipsaNu.Application.Commons.Interfaces;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.ExtraBets.DTOs;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.Features.ExtraBets.Queries.GetExtraBetOptionsForUser
{
    public class GetExtraBetOptionsForUserQueryHandler
        : IRequestHandler<GetExtraBetOptionsForUserQuery, OperationResult<List<ExtraBetOptionForUserDto>>>
    {
        private readonly IExtraBetRepository _extraBetRepository;
        private readonly IGenericRepository<Tournament> _genericTournamentRepository;
        private readonly ICurrentUserService _currentUser;
        private readonly IMapper _mapper;

        public GetExtraBetOptionsForUserQueryHandler(
            IExtraBetRepository extraBetRepository,
            IGenericRepository<Tournament> genericTournamentRepository,
            ICurrentUserService currentUser,
            IMapper mapper)
        {
            _extraBetRepository = extraBetRepository;
            _genericTournamentRepository = genericTournamentRepository;
            _currentUser = currentUser;
            _mapper = mapper;
        }

        public async Task<OperationResult<List<ExtraBetOptionForUserDto>>> Handle(
            GetExtraBetOptionsForUserQuery request,
            CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId;
            if (userId <= 0)
                return OperationResult<List<ExtraBetOptionForUserDto>>.Failure("Unauthorized");

            var tournament = await _genericTournamentRepository.GetByIdAsync(request.TournamentId, cancellationToken);
            if (tournament == null)
                return OperationResult<List<ExtraBetOptionForUserDto>>.Failure("Tournament not found");

            var options = await _extraBetRepository
                .GetOptionsWithUserBetAsync(request.TournamentId, userId, cancellationToken);

            var dtos = _mapper.Map<List<ExtraBetOptionForUserDto>>(options);

            return OperationResult<List<ExtraBetOptionForUserDto>>.Success(dtos);
        }
    }
}