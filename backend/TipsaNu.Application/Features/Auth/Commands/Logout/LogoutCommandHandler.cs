using MediatR;
using TipsaNu.Application.Commons.Results;
using TipsaNu.Application.Features.Auth.Interfaces;

namespace TipsaNu.Application.Features.Auth.Commands.Logout;

public class LogoutCommandHandler(IRefreshTokenService refreshService) 
    : IRequestHandler<LogoutCommand, OperationResult<Unit>>
{
    public async Task<OperationResult<Unit>> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var refreshToken = await refreshService.GetRefreshTokenAsync(request.Request.RefreshToken, cancellationToken);

        if (refreshToken != null)
        {
            await refreshService.DeleteRefreshTokenAsync(refreshToken, cancellationToken);
        }

        return OperationResult<Unit>.Success(Unit.Value);
    }
}