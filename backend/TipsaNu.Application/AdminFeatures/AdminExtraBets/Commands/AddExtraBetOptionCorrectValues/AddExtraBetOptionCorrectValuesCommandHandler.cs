using MediatR;
using TipsaNu.Application.AdminFeatures.AdminExtraBets.Events;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.AdminFeatures.AdminExtraBets.Commands.AddExtraBetOptionCorrectValues
{
    public class AddExtraBetOptionCorrectValuesCommandHandler
        : IRequestHandler<AddExtraBetOptionCorrectValuesCommand, OperationResult<bool>>
    {
        private readonly IExtraBetRepository _extraBetRepository;
        private readonly IGenericRepository<ExtraBetOption> _genericExtraBetOptionRepository;
        private readonly IMediator _mediator;

        public AddExtraBetOptionCorrectValuesCommandHandler(
            IExtraBetRepository extraBetRepository,
            IGenericRepository<ExtraBetOption> genericExtraBetOptionRepository,
            IMediator mediator)
        {
            _extraBetRepository = extraBetRepository;
            _genericExtraBetOptionRepository = genericExtraBetOptionRepository;
            _mediator = mediator;
        }

        public async Task<OperationResult<bool>> Handle(AddExtraBetOptionCorrectValuesCommand request, CancellationToken cancellationToken)
        {
            var option = await _genericExtraBetOptionRepository.GetByIdAsync(request.OptionId, cancellationToken);
            if (option == null)
                return OperationResult<bool>.Failure("ExtraBetOption not found");

            var existingValues = await _extraBetRepository.GetCorrectValuesByOptionIdAsync(request.OptionId, cancellationToken);
            var existingValueStrings = existingValues.Select(ev => ev.Value).ToHashSet();

            var valuesToAdd = request.SetExtraBetOptionCorrectValuesDto.CorrectValues
                .Where(v => !existingValueStrings.Contains(v));

            foreach (var value in valuesToAdd)
            {
                await _extraBetRepository.AddCorrectValueAsync(request.OptionId, value, cancellationToken);
            }

            await _mediator.Publish(new ExtraBetOptionCorrectValuesUpdatedEvent(request.OptionId), cancellationToken);

            return OperationResult<bool>.Success(true);
        }
    }
}