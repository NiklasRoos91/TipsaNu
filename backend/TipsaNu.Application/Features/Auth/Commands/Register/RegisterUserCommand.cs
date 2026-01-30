using MediatR;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Auth.DTOs;

namespace TipsaNu.Application.Feature.Auth.Commands.Register
{
    public record RegisterUserCommand(RegisterRequestDto Request)
        : IRequest<OperationResult<AuthResponseDto>>;
}
