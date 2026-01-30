using MediatR;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Auth.DTOs;

namespace TipsaNu.Application.Features.Auth.Commands.Login
{
    public record LoginUserCommand(LoginRequestDto Request)
        : IRequest<OperationResult<AuthResponseDto>>;
}
