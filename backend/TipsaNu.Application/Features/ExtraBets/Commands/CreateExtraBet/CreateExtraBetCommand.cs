using MediatR;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.ExtraBets.DTOs;

namespace TipsaNu.Application.Features.ExtraBets.Commands.CreateExtraBet
{
    public record CreateExtraBetCommand(int OptionId, CreateExtraBetDto CreateExtraBetDto)
        : IRequest<OperationResult<ExtraBetDto>>;
}
