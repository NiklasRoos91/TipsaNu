using AutoMapper;
using MediatR;
using TipsaNu.Application.Commons.Interfaces;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.ExtraBets.DTOs;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.Features.ExtraBets.Queries.GetMyExtraBetByOptionId
{
    public class GetMyExtraBetByOptionIdQueryHandler
        : IRequestHandler<GetMyExtraBetByOptionIdQuery, OperationResult<ExtraBetDto>>
    {
        private readonly IExtraBetRepository _extraBetRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public GetMyExtraBetByOptionIdQueryHandler(
            IExtraBetRepository extraBetRepository,
            ICurrentUserService currentUserService,
            IMapper mapper)
        {
            _extraBetRepository = extraBetRepository;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<OperationResult<ExtraBetDto>> Handle(
            GetMyExtraBetByOptionIdQuery request,
            CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            if (userId <= 0)
            {
                return OperationResult<ExtraBetDto>.Failure("User is not authenticated.");
            }

            var entity = await _extraBetRepository.GetMyExtraBetByOptionIdAsync(
                request.OptionId,
                userId,
                cancellationToken);

            if (entity is null)
            {
                return OperationResult<ExtraBetDto>.Failure(
                    $"No bet found for optionId={request.OptionId} for this user.");
            }

            var dto = _mapper.Map<ExtraBetDto>(entity);

            return OperationResult<ExtraBetDto>.Success(dto);
        }
    }
}
