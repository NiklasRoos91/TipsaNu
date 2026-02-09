using MediatR;
using TipsaNu.Application.AdminFeatures.AdminExtraBets.Events;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.AdminFeatures.AdminExtraBets.Commands.ReplaceExtraBetOptionCorrectValues
{
    public class ReplaceExtraBetOptionCorrectValuesCommandHandler
    : IRequestHandler<ReplaceExtraBetOptionCorrectValuesCommand, OperationResult<Unit>>
    {
        private readonly IExtraBetRepository _extraBetRepository;
        private readonly IMediator _mediator;

        public ReplaceExtraBetOptionCorrectValuesCommandHandler(IExtraBetRepository extraBetRepository, IMediator mediator)
        {
            _extraBetRepository = extraBetRepository;
            _mediator = mediator;
        }

        public async Task<OperationResult<Unit>> Handle(ReplaceExtraBetOptionCorrectValuesCommand request, CancellationToken cancellationToken)
        {
            var existingValues = await _extraBetRepository.GetCorrectValuesByOptionIdAsync(request.OptionId, cancellationToken);

            await _extraBetRepository.RemoveCorrectValuesAsync(request.OptionId, cancellationToken);

            foreach (var value in request.SetExtraBetOptionCorrectValuesDto.CorrectValues)
            {
                await _extraBetRepository.AddCorrectValueAsync(request.OptionId, value, cancellationToken);
            }

            await _mediator.Publish(new ExtraBetOptionCorrectValuesUpdatedEvent(request.OptionId), cancellationToken);

            return OperationResult<Unit>.Success(Unit.Value);
        }
    }
}