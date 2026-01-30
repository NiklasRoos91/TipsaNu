using MediatR;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Feature.Auth.DTOs;
using TipsaNu.Application.Feature.Auth.Interfaces;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.Feature.Auth.Commands.Login
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, OperationResult<AuthResponseDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenService _jwt;
        private readonly IRefreshTokenService _refresh;
        private readonly IPasswordService _passwordService;

        public LoginUserCommandHandler(
            IUserRepository userRepository,
            IJwtTokenService jwt,
            IRefreshTokenService refresh,
            IPasswordService passwordService)
        {
            _userRepository = userRepository;
            _jwt = jwt;
            _refresh = refresh;
            _passwordService = passwordService;
        }

        public async Task<OperationResult<AuthResponseDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Request.Email);
            if (user == null || !_passwordService.Verify(user.PasswordHash, request.Request.Password))
                return OperationResult<AuthResponseDto>.Failure("Wrong email or password");

            var accessToken = _jwt.GenerateToken(user);
            var refreshToken = await _refresh.CreateRefreshTokenAsync(user);

            var response = new AuthResponseDto(accessToken, refreshToken.Token);
            return OperationResult<AuthResponseDto>.Success(response);
        }
    }
}
