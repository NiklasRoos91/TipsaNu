using MediatR;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Auth.DTOs;

namespace TipsaNu.Application.Features.Auth.Queries.GetMe
{
    public record GetMeQuery() : IRequest<OperationResult<UserDto>>;
}
