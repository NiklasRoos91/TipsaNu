using MediatR;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Auth.DTOs;

namespace TipsaNu.Application.Features.Auth.Commands.Logout;

public record LogoutCommand(RefreshTokenRequestDto Request) : IRequest<OperationResult<Unit>>;
