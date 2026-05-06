using MediatR;
using TipsaNu.Application.Commons.Interfaces;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.ExtraBets.DTOs;
using TipsaNu.Application.Features.ExtraBets.Mappers;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.Features.ExtraBets.Queries.GetMyExtraBetByOptionId
{
    public class GetMyExtraBetByOptionIdQueryHandler(
        IExtraBetRepository extraBetRepository,
        ICurrentUserService currentUserService)
        : IRequestHandler<GetMyExtraBetByOptionIdQuery, OperationResult<ExtraBetDto>>
    {
        public async Task<OperationResult<ExtraBetDto>> Handle(
            GetMyExtraBetByOptionIdQuery request,
            CancellationToken cancellationToken)
        {
            var userId = currentUserService.UserId;

            if (userId <= 0)
            {
                return OperationResult<ExtraBetDto>.Failure("User is not authenticated.");
            }

            var entity = await extraBetRepository.GetMyExtraBetByOptionIdAsync(
                request.OptionId,
                userId,
                cancellationToken);

            if (entity is null)
            {
                return OperationResult<ExtraBetDto>.Success(null!);
            }

            return OperationResult<ExtraBetDto>.Success(entity.ToDto());
        }
    }
}
