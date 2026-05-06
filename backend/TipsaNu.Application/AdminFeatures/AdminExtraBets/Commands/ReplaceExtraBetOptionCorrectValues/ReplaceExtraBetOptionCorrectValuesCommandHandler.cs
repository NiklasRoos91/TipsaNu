using MediatR;
using TipsaNu.Application.AdminFeatures.AdminExtraBets.Events;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.AdminFeatures.AdminExtraBets.Commands.ReplaceExtraBetOptionCorrectValues
{
    public class ReplaceExtraBetOptionCorrectValuesCommandHandler(
        IExtraBetRepository extraBetRepository,
        IGenericRepository<ExtraBetOption> genericExtraBetOptionRepository,
        IMediator mediator)
        : IRequestHandler<ReplaceExtraBetOptionCorrectValuesCommand, OperationResult<bool>>
    {
        public async Task<OperationResult<bool>> Handle(ReplaceExtraBetOptionCorrectValuesCommand request, CancellationToken cancellationToken)
        {
            var option = await genericExtraBetOptionRepository.GetByIdAsync(request.OptionId, cancellationToken);
            if (option == null)
                return OperationResult<bool>.Failure("ExtraBetOption not found");

            var existingValues = await extraBetRepository.GetCorrectValuesByOptionIdAsync(request.OptionId, cancellationToken);

            await extraBetRepository.RemoveCorrectValuesAsync(request.OptionId, cancellationToken);

            foreach (var value in request.SetExtraBetOptionCorrectValuesDto.CorrectValues)
            {
                await extraBetRepository.AddCorrectValueAsync(request.OptionId, value, cancellationToken);
            }

            await mediator.Publish(new ExtraBetOptionCorrectValuesUpdatedEvent(request.OptionId), cancellationToken);

            return OperationResult<bool>.Success(true);
        }
    }
}