using MediatR;
using TipsaNu.Application.AdminFeatures.AdminExtraBets.Events;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Enums;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.AdminFeatures.AdminExtraBets.Commands.DeleteSingleExtraBetOptionCorrectValue
{
    public class DeleteSingleExtraBetOptionCorrectValueCommandHandler
        : IRequestHandler<DeleteSingleExtraBetOptionCorrectValueCommand, OperationResult<bool>>
    {
        private readonly IGenericRepository<ExtraBetOptionCorrectValue> _extraBetOptionCorrectValueRepository;
        private readonly IGenericRepository<ExtraBetOption> _extraBetOptionGenericRepository;
        private readonly IExtraBetRepository _extraBetRepository;
        private readonly IMediator _mediator;

        public DeleteSingleExtraBetOptionCorrectValueCommandHandler(
            IGenericRepository<ExtraBetOptionCorrectValue> extraBetOptionCorrectValueRepository,
            IGenericRepository<ExtraBetOption> extraBetOptionGenericRepository,
            IExtraBetRepository extraBetRepository,
            IMediator mediator)
        {
            _extraBetOptionCorrectValueRepository = extraBetOptionCorrectValueRepository;
            _extraBetOptionGenericRepository = extraBetOptionGenericRepository;
            _extraBetRepository = extraBetRepository;
            _mediator = mediator;
        }

        public async Task<OperationResult<bool>> Handle(
            DeleteSingleExtraBetOptionCorrectValueCommand request,
            CancellationToken cancellationToken)
        {
            var entity = await _extraBetOptionCorrectValueRepository.GetByIdAsync(request.CorrectValueId, cancellationToken);
            if (entity == null)
                return OperationResult<bool>.Failure("CorrectValue not found.");

            await _extraBetOptionCorrectValueRepository.DeleteAsync(entity.CorrectValueId, cancellationToken);

            var remainingValues = await _extraBetRepository.GetCorrectValuesByOptionIdAsync(entity.OptionId, cancellationToken);

            if (!remainingValues.Any())
            {
                // Hämta option via generisk repository
                var option = await _extraBetOptionGenericRepository.GetByIdAsync(entity.OptionId, cancellationToken);
                if (option != null)
                {
                    // Ändra status
                    option.Status = ExtraBetOptionStatus.Open;

                    // Spara ändringen
                    await _extraBetOptionGenericRepository.UpdateAsync(option, cancellationToken);
                }
            }

            await _mediator.Publish(new ExtraBetOptionCorrectValuesUpdatedEvent(entity.OptionId), cancellationToken);

            return OperationResult<bool>.Success(true);
        }
    }
}
