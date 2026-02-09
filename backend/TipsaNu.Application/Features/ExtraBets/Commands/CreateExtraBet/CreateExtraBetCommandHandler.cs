using AutoMapper;
using MediatR;
using TipsaNu.Application.Commons.Interfaces;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.ExtraBets.DTOs;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.Features.ExtraBets.Commands.CreateExtraBet
{
    public class CreateExtraBetCommandHandler
    : IRequestHandler<CreateExtraBetCommand, OperationResult<ExtraBetForUserDto>>
    {
        private readonly IExtraBetRepository _extraBetRepository;
        private readonly IGenericRepository<ExtraBet> _genericExtraBetRepository;
        private readonly ICurrentUserService _currentUser;
        private readonly IMapper _mapper;

        public CreateExtraBetCommandHandler(
            IExtraBetRepository extraBetRepository,
            IGenericRepository<ExtraBet> genericExtraBetRepository,
            ICurrentUserService currentUser,
            IMapper mapper)
        {
            _extraBetRepository = extraBetRepository;
            _genericExtraBetRepository = genericExtraBetRepository;
            _currentUser = currentUser;
            _mapper = mapper;
        }

        public async Task<OperationResult<ExtraBetForUserDto>> Handle(CreateExtraBetCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId;
            if (userId <= 0)
                return OperationResult<ExtraBetForUserDto>.Failure("Unauthorized");

            var option = await _extraBetRepository
                .GetOptionByIdWithChoicesAsync(request.OptionId, cancellationToken);

            if (option == null)
                return OperationResult<ExtraBetForUserDto>.Failure("ExtraBetOption not found");

            if (option.ExpiresAt.HasValue && option.ExpiresAt.Value <= DateTime.UtcNow)
                return OperationResult<ExtraBetForUserDto>.Failure("ExtraBetOption has expired");

            var alreadyPlaced = await _extraBetRepository
                .UserHasBetOnOptionAsync(userId, request.OptionId, cancellationToken);
            if (alreadyPlaced)
                return OperationResult<ExtraBetForUserDto>.Failure("You have already placed a bet for this option");

            if (!option.AllowCustomChoice && !option.ExtraBetOptionChoices.Any(c => c.Value == request.CreateExtraBetDto.Value.Trim()))            
                return OperationResult<ExtraBetForUserDto>.Failure("Invalid value for this ExtraBetOption");

            var extraBet = _mapper.Map<ExtraBet>(request.CreateExtraBetDto);
            extraBet.UserId = userId;
            extraBet.OptionId = request.OptionId;

            await _genericExtraBetRepository.AddAsync(extraBet, cancellationToken);

            var dto = _mapper.Map<ExtraBetForUserDto>(extraBet);

            return OperationResult<ExtraBetForUserDto>.Success(dto);
        }
    }
}