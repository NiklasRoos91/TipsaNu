using MediatR;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Auth.Interfaces;
using TipsaNu.Application.Features.Auth.DTOs;

namespace TipsaNu.Application.Features.Auth.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler(
        IRefreshTokenService refreshService,
        IJwtTokenService jwt)
        : IRequestHandler<RefreshTokenCommand, OperationResult<AuthResponseDto>>
    {
        public async Task<OperationResult<AuthResponseDto>> Handle(
            RefreshTokenCommand request,
            CancellationToken cancellationToken)
        {
            var refreshToken = await refreshService
                .GetRefreshTokenAsync(request.Request.RefreshToken, cancellationToken);

            if (refreshToken == null)
                return OperationResult<AuthResponseDto>
                    .Failure("Invalid or expired refresh token");

            await refreshService.DeleteRefreshTokenAsync(refreshToken, cancellationToken);

            var accessToken = jwt.GenerateToken(refreshToken.User);
            var newRefreshToken = await refreshService
                .CreateRefreshTokenAsync(refreshToken.User, cancellationToken);

            return OperationResult<AuthResponseDto>.Success(
                new AuthResponseDto(accessToken, newRefreshToken.Token)
            );
        }
    }
}