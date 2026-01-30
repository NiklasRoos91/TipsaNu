using MediatR;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Feature.Auth.DTOs;
using TipsaNu.Application.Feature.Auth.Interfaces;

namespace TipsaNu.Application.Feature.Auth.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler
    : IRequestHandler<RefreshTokenCommand, OperationResult<AuthResponseDto>>
    {
        private readonly IRefreshTokenService _refreshService;
        private readonly IJwtTokenService _jwt;

        public RefreshTokenCommandHandler(
            IRefreshTokenService refreshService,
            IJwtTokenService jwt)
        {
            _refreshService = refreshService;
            _jwt = jwt;
        }

        public async Task<OperationResult<AuthResponseDto>> Handle(
            RefreshTokenCommand request,
            CancellationToken cancellationToken)
        {
            var refreshToken = await _refreshService
                .GetRefreshTokenAsync(request.Request.RefreshToken);

            if (refreshToken == null)
                return OperationResult<AuthResponseDto>
                    .Failure("Invalid or expired refresh token");

            // rotera tokens (best practice)
            await _refreshService.RevokeRefreshTokenAsync(refreshToken);

            var accessToken = _jwt.GenerateToken(refreshToken.User);
            var newRefreshToken = await _refreshService
                .CreateRefreshTokenAsync(refreshToken.User);

            return OperationResult<AuthResponseDto>.Success(
                new AuthResponseDto(accessToken, newRefreshToken.Token)
            );
        }
    }
}