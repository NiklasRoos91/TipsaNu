using MediatR;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Feature.Auth.DTOs;

namespace TipsaNu.Application.Feature.Auth.Commands.RefreshToken
{
    public record RefreshTokenCommand(RefreshTokenRequestDto Request)
        : IRequest<OperationResult<AuthResponseDto>>;
}
