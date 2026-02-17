using MediatR;
using TipsaNu.Application.AdminFeatures.AdminExtraBets.Events;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Enums;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.AdminFeatures.AdminExtraBets.Commands.DeleteExtraBetOptionCorrectValues
{
    public class DeleteExtraBetOptionCorrectValuesCommandHandler
    : IRequestHandler<DeleteExtraBetOptionCorrectValuesCommand, OperationResult<bool>>
    {
        private readonly IExtraBetRepository _extraBetRepository;
        private readonly IGenericRepository<ExtraBetOption> _genericExtraBetOptionRepository;
        private readonly IMediator _mediator;

        public DeleteExtraBetOptionCorrectValuesCommandHandler(
            IExtraBetRepository extraBetRepository, 
            IGenericRepository<ExtraBetOption> genericExtraBetOptionRepository,
            IMediator mediator)
        {
            _extraBetRepository = extraBetRepository;
            _genericExtraBetOptionRepository = genericExtraBetOptionRepository;
            _mediator = mediator;
        }

        public async Task<OperationResult<bool>> Handle(DeleteExtraBetOptionCorrectValuesCommand request, CancellationToken cancellationToken)
        {
            var existingValues = await _extraBetRepository.GetCorrectValuesByOptionIdAsync(request.OptionId, cancellationToken);

            if (!existingValues.Any())
                return OperationResult<bool>.Failure("No correct values found to delete.");

            await _extraBetRepository.RemoveCorrectValuesAsync(request.OptionId, cancellationToken);

            var option = await _genericExtraBetOptionRepository.GetByIdAsync(request.OptionId, cancellationToken);
            if (option != null)
            {
                option.Status = ExtraBetOptionStatus.Open;
                await _genericExtraBetOptionRepository.UpdateAsync(option, cancellationToken);
            }

            await _mediator.Publish(new ExtraBetOptionCorrectValuesUpdatedEvent(request.OptionId), cancellationToken);

            return OperationResult<bool>.Success(true);
        }
    }
}
