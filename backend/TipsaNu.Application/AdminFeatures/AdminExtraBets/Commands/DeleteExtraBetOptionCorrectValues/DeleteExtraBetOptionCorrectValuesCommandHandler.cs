using MediatR;
using TipsaNu.Application.AdminFeatures.AdminExtraBets.Events;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Enums;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.AdminFeatures.AdminExtraBets.Commands.DeleteExtraBetOptionCorrectValues
{
    public class DeleteExtraBetOptionCorrectValuesCommandHandler(
        IExtraBetRepository extraBetRepository,
        IGenericRepository<ExtraBetOption> genericExtraBetOptionRepository,
        IMediator mediator)
        : IRequestHandler<DeleteExtraBetOptionCorrectValuesCommand, OperationResult<bool>>
    {
        public async Task<OperationResult<bool>> Handle(DeleteExtraBetOptionCorrectValuesCommand request, CancellationToken cancellationToken)
        {
            var existingValues = await extraBetRepository.GetCorrectValuesByOptionIdAsync(request.OptionId, cancellationToken);

            if (!existingValues.Any())
                return OperationResult<bool>.Failure("No correct values found to delete.");

            await extraBetRepository.RemoveCorrectValuesAsync(request.OptionId, cancellationToken);

            var option = await genericExtraBetOptionRepository.GetByIdAsync(request.OptionId, cancellationToken);
            if (option != null)
            {
                option.Status = ExtraBetOptionStatus.Open;
                await genericExtraBetOptionRepository.UpdateAsync(option, cancellationToken);
            }

            await mediator.Publish(new ExtraBetOptionCorrectValuesUpdatedEvent(request.OptionId), cancellationToken);

            return OperationResult<bool>.Success(true);
        }
    }
}
