using AutoMapper;
using MediatR;
using TipsaNu.Application.AdminFeatures.AdminExtraBets.DTOs;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.AdminFeatures.AdminExtraBets.Commands.CreateExtraBetOption
{
    public class CreateExtraBetOptionCommandHandler
        : IRequestHandler<CreateExtraBetOptionCommand, OperationResult<ExtraBetOptionDto>>
    {
        private readonly IExtraBetRepository _repository;
        private readonly IMapper _mapper;

        public CreateExtraBetOptionCommandHandler(IExtraBetRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<OperationResult<ExtraBetOptionDto>> Handle(
            CreateExtraBetOptionCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Dto;

            if (dto.Points < 0)
                return OperationResult<ExtraBetOptionDto>.Failure("Points must be >= 0");

            var newOption = new ExtraBetOption
            {
                TournamentId = dto.TournamentId,
                MatchId = dto.MatchId,
                Name = dto.Name,
                Description = dto.Description,
                Points = dto.Points,
                ExpiresAt = dto.ExpiresAt
            };

            var createdOption = await _repository.AddExtraBetOptionAsync(newOption, cancellationToken);

            if (dto.Choices != null)
            {
                foreach (var choice in dto.Choices)
                {
                    await _repository.AddExtraBetOptionChoiceAsync(createdOption.OptionId, choice, cancellationToken);
                }
            }

            var resultDto = _mapper.Map<ExtraBetOptionDto>(createdOption);
            return OperationResult<ExtraBetOptionDto>.Success(resultDto);
        }
    }
}