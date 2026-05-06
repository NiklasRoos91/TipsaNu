using MediatR;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Auth.DTOs;
using TipsaNu.Application.Features.Auth.Interfaces;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Enums;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.Feature.Auth.Commands.Register
{
    public class RegisterUserCommandHandler(
        IUserRepository userRepository,
        IGenericRepository<User> genericInterface,
        IJwtTokenService jwt,
        IRefreshTokenService refresh,
        IPasswordService passwordService)
        : IRequestHandler<RegisterUserCommand, OperationResult<AuthResponseDto>>
    {
        public async Task<OperationResult<AuthResponseDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await userRepository.GetByEmailAsync(request.Request.Email, cancellationToken);
            if (existingUser != null)
                return OperationResult<AuthResponseDto>.Failure("Email is already in use");

            var user = new User
            {
                Email = request.Request.Email,
                UserName = request.Request.Username,
                Role = UserRoleEnum.User,
                PasswordHash = passwordService.Hash(request.Request.Password),
                CreatedAt = DateTime.UtcNow
            };

            await genericInterface.AddAsync(user, cancellationToken);

            var accessToken = jwt.GenerateToken(user);
            var refreshToken = await refresh.CreateRefreshTokenAsync(user, cancellationToken);

            var response = new AuthResponseDto(accessToken, refreshToken.Token);
            return OperationResult<AuthResponseDto>.Success(response);
        }
    }
}
