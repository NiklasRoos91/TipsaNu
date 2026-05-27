using MediatR;
using TipsaNu.Application.Features.Auth.Interfaces;

namespace TipsaNu.Application.Features.Auth.Commands.ClearExpiredTokens;

public class ClearExpiredTokensCommandHandler(IRefreshTokenService refreshService) 
    : IRequestHandler<ClearExpiredTokensCommand, Unit>
{
    public async Task<Unit> Handle(ClearExpiredTokensCommand request, CancellationToken cancellationToken)
    {
        await refreshService.ClearExpiredRefreshTokensAsync(cancellationToken);
        return Unit.Value;
    }
}