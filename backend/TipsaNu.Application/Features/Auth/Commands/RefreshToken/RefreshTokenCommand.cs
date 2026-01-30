using MediatR;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Auth.DTOs;

namespace TipsaNu.Application.Features.Auth.Commands.RefreshToken
{
    public record RefreshTokenCommand(RefreshTokenRequestDto Request)
        : IRequest<OperationResult<AuthResponseDto>>;
}
