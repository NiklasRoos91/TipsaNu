using MediatR;
using TipsaNu.Application.AdminFeatures.AdminExtraBets.Events;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Enums;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.AdminFeatures.AdminExtraBets.Commands.AddExtraBetOptionCorrectValues
{
    public class AddExtraBetOptionCorrectValuesCommandHandler(
        IExtraBetRepository extraBetRepository,
        IGenericRepository<ExtraBetOption> genericExtraBetOptionRepository,
        IMediator mediator)
        : IRequestHandler<AddExtraBetOptionCorrectValuesCommand, OperationResult<bool>>
    {
        public async Task<OperationResult<bool>> Handle(AddExtraBetOptionCorrectValuesCommand request, CancellationToken cancellationToken)
        {
            var option = await genericExtraBetOptionRepository.GetByIdAsync(request.OptionId, cancellationToken);
            if (option == null)
                return OperationResult<bool>.Failure("ExtraBetOption not found");

            var existingValues = await extraBetRepository.GetCorrectValuesByOptionIdAsync(request.OptionId, cancellationToken);

            var existingValueStrings = existingValues
                .Select(ev => ev.Value.Trim())
                .ToHashSet(StringComparer.OrdinalIgnoreCase);
            
            var uniqueNewValuesToSave = request.SetExtraBetOptionCorrectValuesDto.CorrectValues
                .Select(v => v.Trim())
                .Where(trimmedValue => !existingValueStrings.Contains(trimmedValue))
                .Select(trimmedValue => new ExtraBetOptionCorrectValue
                {
                    OptionId = request.OptionId,
                    Value = trimmedValue
                })
                .ToList();

            if (uniqueNewValuesToSave.Any())
            {
                await extraBetRepository.AddCorrectValuesRangeAsync(uniqueNewValuesToSave, cancellationToken);

                option.Status = ExtraBetOptionStatus.Closed;
                await genericExtraBetOptionRepository.UpdateAsync(option, cancellationToken);
                
            }

            await mediator.Publish(new ExtraBetOptionCorrectValuesUpdatedEvent(request.OptionId), cancellationToken);
            
            return OperationResult<bool>.Success(true);
        }
    }
}