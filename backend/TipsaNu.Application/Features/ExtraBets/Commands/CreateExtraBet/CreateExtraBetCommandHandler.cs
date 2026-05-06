using MediatR;
using TipsaNu.Application.Commons.Interfaces;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.ExtraBets.DTOs;
using TipsaNu.Application.Features.ExtraBets.Mappers;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.Features.ExtraBets.Commands.CreateExtraBet
{
    public class CreateExtraBetCommandHandler(
        IExtraBetRepository extraBetRepository,
        IGenericRepository<ExtraBet> genericExtraBetRepository,
        ICurrentUserService currentUser)
        : IRequestHandler<CreateExtraBetCommand, OperationResult<ExtraBetDto>>
    {
        public async Task<OperationResult<ExtraBetDto>> Handle(CreateExtraBetCommand request, CancellationToken cancellationToken)
        {
            var userId = currentUser.UserId;
            if (userId <= 0)
                return OperationResult<ExtraBetDto>.Failure("Unauthorized");

            var option = await extraBetRepository
                .GetOptionByIdWithChoicesAsync(request.OptionId, cancellationToken);

            if (option == null)
                return OperationResult<ExtraBetDto>.Failure("ExtraBetOption not found");

            if (option.ExpiresAt.HasValue && option.ExpiresAt.Value <= DateTime.UtcNow)
                return OperationResult<ExtraBetDto>.Failure("ExtraBetOption has expired");

            var alreadyPlaced = await extraBetRepository
                .UserHasBetOnOptionAsync(userId, request.OptionId, cancellationToken);
            if (alreadyPlaced)
                return OperationResult<ExtraBetDto>.Failure("You have already placed a bet for this option");

            if (!option.AllowCustomChoice && option.ExtraBetOptionChoices.All(c => c.Value != request.CreateExtraBetDto.Value.Trim()))            
                return OperationResult<ExtraBetDto>.Failure("Invalid value for this ExtraBetOption");

            var extraBet = new ExtraBet
            {
                Value = request.CreateExtraBetDto.Value.Trim(),
                UserId = userId,
                OptionId = request.OptionId,
            };
            
            await genericExtraBetRepository.AddAsync(extraBet, cancellationToken);
            
            return OperationResult<ExtraBetDto>.Success(extraBet.ToDto());
        }
    }
}