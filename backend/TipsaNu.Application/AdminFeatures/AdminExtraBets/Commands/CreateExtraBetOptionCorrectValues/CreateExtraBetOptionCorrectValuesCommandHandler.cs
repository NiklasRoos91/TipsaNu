using MediatR;
using TipsaNu.Application.AdminFeatures.AdminExtraBets.Events;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Enums;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.AdminFeatures.AdminExtraBets.Commands.CreateExtraBetOptionCorrectValues
{
    public class CreateExtraBetOptionCorrectValuesCommandHandler(
        IExtraBetRepository extraBetRepository,
        IGenericRepository<ExtraBetOption> genericExtraBetOptionRepository,
        IMediator mediator)
        : IRequestHandler<CreateExtraBetOptionCorrectValuesCommand, OperationResult<bool>>
    {
        public async Task<OperationResult<bool>> Handle(CreateExtraBetOptionCorrectValuesCommand request, CancellationToken cancellationToken)
        {
            var option = await genericExtraBetOptionRepository.GetByIdAsync(request.OptionId, cancellationToken);
            if (option == null)
                return OperationResult<bool>.Failure("ExtraBetOption not found");

            var existingValues = await extraBetRepository.GetCorrectValuesByOptionIdAsync(request.OptionId, cancellationToken);
            if (existingValues.Any())
                return OperationResult<bool>.Failure("Correct values already exist, use PATCH to update.");

            foreach (var value in request.SetExtraBetOptionCorrectValuesDto.CorrectValues)
            {
                await extraBetRepository.AddCorrectValueAsync(request.OptionId, value, cancellationToken);
            }

            option.Status = ExtraBetOptionStatus.Closed;
            await genericExtraBetOptionRepository.UpdateAsync(option, cancellationToken);

            await mediator.Publish(new ExtraBetOptionCorrectValuesUpdatedEvent(request.OptionId), cancellationToken);

            return OperationResult<bool>.Success(true);
        }
    }
}