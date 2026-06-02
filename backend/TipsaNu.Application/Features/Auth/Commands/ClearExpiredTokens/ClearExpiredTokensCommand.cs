using MediatR;

namespace TipsaNu.Application.Features.Auth.Commands.ClearExpiredTokens;

public record ClearExpiredTokensCommand : IRequest<Unit>;