using AutoMapper;
using MediatR;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.ExtraBets.DTOs;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.AdminFeatures.AdminExtraBets.Commands.CreateExtraBetOption
{
    public class CreateExtraBetOptionCommandHandler
        : IRequestHandler<CreateExtraBetOptionCommand, OperationResult<ExtraBetOptionDto>>
    {
        private readonly IExtraBetRepository _extraBetsRepository;
        private readonly IGenericRepository<Tournament> _genericTournamentRepository;
        private readonly IMapper _mapper;

        public CreateExtraBetOptionCommandHandler(IExtraBetRepository extraBetsRepository, IGenericRepository<Tournament> genericTournamentRepository, IMapper mapper)
        {
            _extraBetsRepository = extraBetsRepository;
            _genericTournamentRepository = genericTournamentRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<ExtraBetOptionDto>> Handle(
            CreateExtraBetOptionCommand request, CancellationToken cancellationToken)
        {
            if (request.Dto.Points < 0)
                return OperationResult<ExtraBetOptionDto>.Failure("Points must be >= 0");

            var tournament = await _genericTournamentRepository.GetByIdAsync(request.Dto.TournamentId, cancellationToken);
            if (tournament == null)
                return OperationResult<ExtraBetOptionDto>.Failure("Tournament not found");

            var newOption = new ExtraBetOption
            {
                TournamentId = request.Dto.TournamentId,
                MatchId = request.Dto.MatchId,
                Name = request.Dto.Name,
                Description = request.Dto.Description,
                Points = request.Dto.Points,
                ExpiresAt = request.Dto.ExpiresAt,
                AllowCustomChoice = request.Dto.AllowCustomChoice
            };

            var createdOption = await _extraBetsRepository.AddExtraBetOptionAsync(newOption, cancellationToken);

            if (request.Dto.Choices != null)
            {
                foreach (var choice in request.Dto.Choices)
                {
                    await _extraBetsRepository.AddExtraBetOptionChoiceAsync(createdOption.OptionId, choice, cancellationToken);
                }
            }

            var resultDto = _mapper.Map<ExtraBetOptionDto>(createdOption);
            return OperationResult<ExtraBetOptionDto>.Success(resultDto);
        }
    }
}