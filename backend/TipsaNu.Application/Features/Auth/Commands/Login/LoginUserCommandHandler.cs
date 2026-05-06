using MediatR;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Auth.DTOs;
using TipsaNu.Application.Features.Auth.Interfaces;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.Features.Auth.Commands.Login
{
    public class LoginUserCommandHandler(
        IUserRepository userRepository,
        IJwtTokenService jwt,
        IRefreshTokenService refresh,
        IPasswordService passwordService)
        : IRequestHandler<LoginUserCommand, OperationResult<AuthResponseDto>>
    {
        public async Task<OperationResult<AuthResponseDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByEmailAsync(request.Request.Email, cancellationToken);
            if (user == null || !passwordService.Verify(user.PasswordHash, request.Request.Password))
                return OperationResult<AuthResponseDto>.Failure("Wrong email or password");

            var accessToken = jwt.GenerateToken(user);
            var refreshToken = await refresh.CreateRefreshTokenAsync(user);

            var response = new AuthResponseDto(accessToken, refreshToken.Token);
            return OperationResult<AuthResponseDto>.Success(response);
        }
    }
}
