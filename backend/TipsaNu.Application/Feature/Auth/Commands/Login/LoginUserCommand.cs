using MediatR;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Feature.Auth.DTOs;

namespace TipsaNu.Application.Feature.Auth.Commands.Login
{
    public record LoginUserCommand(LoginRequestDto Request)
        : IRequest<OperationResult<AuthResponseDto>>;
}
