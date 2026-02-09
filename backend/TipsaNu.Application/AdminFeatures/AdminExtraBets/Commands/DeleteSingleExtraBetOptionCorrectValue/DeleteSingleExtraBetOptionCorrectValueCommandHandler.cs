using MediatR;
using TipsaNu.Application.AdminFeatures.AdminExtraBets.Events;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.AdminFeatures.AdminExtraBets.Commands.DeleteSingleExtraBetOptionCorrectValue
{
    public class DeleteSingleExtraBetOptionCorrectValueCommandHandler
        : IRequestHandler<DeleteSingleExtraBetOptionCorrectValueCommand, OperationResult<Unit>>
    {
        private readonly IGenericRepository<ExtraBetOptionCorrectValue> _repository;
        private readonly IMediator _mediator;

        public DeleteSingleExtraBetOptionCorrectValueCommandHandler(
            IGenericRepository<ExtraBetOptionCorrectValue> repository,
            IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<OperationResult<Unit>> Handle(
            DeleteSingleExtraBetOptionCorrectValueCommand request,
            CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.CorrectValueId, cancellationToken);
            if (entity == null)
                return OperationResult<Unit>.Failure("CorrectValue not found.");

            await _repository.DeleteAsync(entity.CorrectValueId, cancellationToken);

            await _mediator.Publish(new ExtraBetOptionCorrectValuesUpdatedEvent(entity.OptionId), cancellationToken);

            return OperationResult<Unit>.Success(Unit.Value);
        }
    }
}
