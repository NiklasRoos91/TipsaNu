using MediatR;
using TipsaNu.Application.AdminFeatures.AdminExtraBets.Events;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.AdminFeatures.AdminExtraBets.Commands.DeleteExtraBetOptionCorrectValues
{
    public class DeleteExtraBetOptionCorrectValuesCommandHandler
    : IRequestHandler<DeleteExtraBetOptionCorrectValuesCommand, OperationResult<Unit>>
    {
        private readonly IExtraBetRepository _extraBetRepository;
        private readonly IMediator _mediator;

        public DeleteExtraBetOptionCorrectValuesCommandHandler(IExtraBetRepository extraBetRepository, IMediator mediator)
        {
            _extraBetRepository = extraBetRepository;
            _mediator = mediator;
        }

        public async Task<OperationResult<Unit>> Handle(DeleteExtraBetOptionCorrectValuesCommand request, CancellationToken cancellationToken)
        {
            var existingValues = await _extraBetRepository.GetCorrectValuesByOptionIdAsync(request.OptionId, cancellationToken);

            if (!existingValues.Any())
                return OperationResult<Unit>.Failure("No correct values found to delete.");

            await _extraBetRepository.RemoveCorrectValuesAsync(request.OptionId, cancellationToken);

            await _mediator.Publish(new ExtraBetOptionCorrectValuesUpdatedEvent(request.OptionId), cancellationToken);

            return OperationResult<Unit>.Success(Unit.Value);
        }
    }
}
