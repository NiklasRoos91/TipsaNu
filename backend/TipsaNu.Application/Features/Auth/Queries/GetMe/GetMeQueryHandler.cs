using MediatR;
using TipsaNu.Application.Commons.Interfaces;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Auth.DTOs;
using TipsaNu.Domain.Entities;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.Features.Auth.Queries.GetMe
{
    public class GetMeQueryHandler : IRequestHandler<GetMeQuery, OperationResult<UserDto>>
    {
        private readonly IGenericRepository<User> _genericUserRepository;
        private readonly ICurrentUserService _currentUser;

        public GetMeQueryHandler(IGenericRepository<User> genericUserRepository, ICurrentUserService currentUser)
        {
            _genericUserRepository = genericUserRepository;
            _currentUser = currentUser;
        }

        public async Task<OperationResult<UserDto>> Handle(GetMeQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId;

            if (userId == 0)
                return OperationResult<UserDto>.Failure("User not authenticated");

            var user = await _genericUserRepository.GetByIdAsync(userId, cancellationToken);

            if (user == null)
                return OperationResult<UserDto>.Failure("User not found");

            var dto = new UserDto
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role.ToString()
            };

            return OperationResult<UserDto>.Success(dto);
        }
    }

}
