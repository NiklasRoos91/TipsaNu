using MediatR;
using TipsaNu.Application.AdminFeatures.AdminExtraBets.Events;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Enums;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.AdminFeatures.AdminExtraBets.Commands.DeleteSingleExtraBetOptionCorrectValue
{
    public class DeleteSingleExtraBetOptionCorrectValueCommandHandler(
        IGenericRepository<ExtraBetOptionCorrectValue> extraBetOptionCorrectValueRepository,
        IGenericRepository<ExtraBetOption> extraBetOptionGenericRepository,
        IExtraBetRepository extraBetRepository,
        IMediator mediator)
        : IRequestHandler<DeleteSingleExtraBetOptionCorrectValueCommand, OperationResult<bool>>
    {
        public async Task<OperationResult<bool>> Handle(
            DeleteSingleExtraBetOptionCorrectValueCommand request,
            CancellationToken cancellationToken)
        {
            var entity = await extraBetOptionCorrectValueRepository.GetByIdAsync(request.CorrectValueId, cancellationToken);
            if (entity == null)
                return OperationResult<bool>.Failure("CorrectValue not found.");

            await extraBetOptionCorrectValueRepository.DeleteAsync(entity.CorrectValueId, cancellationToken);

            var remainingValues = await extraBetRepository.GetCorrectValuesByOptionIdAsync(entity.OptionId, cancellationToken);

            if (!remainingValues.Any())
            {
                // Hämta option via generisk repository
                var option = await extraBetOptionGenericRepository.GetByIdAsync(entity.OptionId, cancellationToken);
                if (option != null)
                {
                    // Ändra status
                    option.Status = ExtraBetOptionStatus.Open;

                    // Spara ändringen
                    await extraBetOptionGenericRepository.UpdateAsync(option, cancellationToken);
                }
            }

            await mediator.Publish(new ExtraBetOptionCorrectValuesUpdatedEvent(entity.OptionId), cancellationToken);

            return OperationResult<bool>.Success(true);
        }
    }
}
