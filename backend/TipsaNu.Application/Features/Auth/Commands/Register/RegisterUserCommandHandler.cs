using MediatR;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Auth.DTOs;
using TipsaNu.Application.Features.Auth.Interfaces;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Enums;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.Feature.Auth.Commands.Register
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, OperationResult<AuthResponseDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IGenericRepository<User> _genericInterface;
        private readonly IJwtTokenService _jwt;
        private readonly IRefreshTokenService _refresh;
        private readonly IPasswordService _passwordService;

        public RegisterUserCommandHandler(
            IUserRepository userRepository,
            IGenericRepository<User> genericInterface,
            IJwtTokenService jwt,
            IRefreshTokenService refresh,
            IPasswordService passwordService)
        {
            _userRepository = userRepository;
            _genericInterface = genericInterface;
            _jwt = jwt;
            _refresh = refresh;
            _passwordService = passwordService;
        }

        public async Task<OperationResult<AuthResponseDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetByEmailAsync(request.Request.Email, cancellationToken);
            if (existingUser != null)
                return OperationResult<AuthResponseDto>.Failure("Email is already in use");

            var user = new User
            {
                Email = request.Request.Email,
                Username = request.Request.Username,
                Role = UserRoleEnum.User,
                PasswordHash = _passwordService.Hash(request.Request.Password),
                CreatedAt = DateTime.UtcNow
            };

            await _genericInterface.AddAsync(user, cancellationToken);

            var accessToken = _jwt.GenerateToken(user);
            var refreshToken = await _refresh.CreateRefreshTokenAsync(user, cancellationToken);

            var response = new AuthResponseDto(accessToken, refreshToken.Token);
            return OperationResult<AuthResponseDto>.Success(response);
        }
    }
}
