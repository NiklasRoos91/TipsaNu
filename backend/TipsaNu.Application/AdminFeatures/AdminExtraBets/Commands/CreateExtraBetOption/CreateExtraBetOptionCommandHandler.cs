using MediatR;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.ExtraBets.DTOs;
using TipsaNu.Application.Features.ExtraBets.Mappers;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Enums;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.AdminFeatures.AdminExtraBets.Commands.CreateExtraBetOption
{
    public class CreateExtraBetOptionCommandHandler(
        IExtraBetRepository extraBetsRepository,
        IGenericRepository<Tournament> genericTournamentRepository)
        : IRequestHandler<CreateExtraBetOptionCommand, OperationResult<ExtraBetOptionDto>>
    {
        public async Task<OperationResult<ExtraBetOptionDto>> Handle(
            CreateExtraBetOptionCommand request, CancellationToken cancellationToken)
        {
            if (request.CreateExtraBetOptionDto.Points < 0)
                return OperationResult<ExtraBetOptionDto>.Failure("Points must be >= 0");

            var tournament = await genericTournamentRepository.GetByIdAsync(request.CreateExtraBetOptionDto.TournamentId, cancellationToken);
            if (tournament == null)
                return OperationResult<ExtraBetOptionDto>.Failure("Tournament not found");

            var newOption = request.CreateExtraBetOptionDto.ToEntity();
            newOption.Status = ExtraBetOptionStatus.Open;

            var createdOption = await extraBetsRepository.AddExtraBetOptionAsync(newOption, cancellationToken);

            if (request.CreateExtraBetOptionDto.Choices != null)
            {
                foreach (var choice in request.CreateExtraBetOptionDto.Choices)
                {
                    await extraBetsRepository.AddExtraBetOptionChoiceAsync(createdOption.OptionId, choice, cancellationToken);
                }
            }

            var resultDto = createdOption.ToDto();
            return OperationResult<ExtraBetOptionDto>.Success(resultDto);
        }
    }
}