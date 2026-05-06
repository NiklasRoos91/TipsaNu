using MediatR;
using TipsaNu.Application.Commons.Interfaces;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Auth.DTOs;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.Features.Auth.Queries.GetMe
{
    public class GetMeQueryHandler(IGenericRepository<User> genericUserRepository, ICurrentUserService currentUser)
        : IRequestHandler<GetMeQuery, OperationResult<UserDto>>
    {
        public async Task<OperationResult<UserDto>> Handle(GetMeQuery request, CancellationToken cancellationToken)
        {
            var userId = currentUser.UserId;

            if (userId == 0)
                return OperationResult<UserDto>.Failure("User not authenticated");

            var user = await genericUserRepository.GetByIdAsync(userId, cancellationToken);

            if (user == null)
                return OperationResult<UserDto>.Failure("User not found");

            var dto = new UserDto
            {
                UserId = user.UserId,
                Username = user.UserName,
                Email = user.Email,
                Role = user.Role.ToString()
            };

            return OperationResult<UserDto>.Success(dto);
        }
    }

}
