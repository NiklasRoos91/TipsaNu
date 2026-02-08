using FluentValidation;
using TipsaNu.Application.Features.Auth.DTOs;

namespace TipsaNu.Application.Features.Auth.Commands.RefreshToken
{
    public class RefreshTokenRequestValidator : AbstractValidator<RefreshTokenRequestDto>
    {
        public RefreshTokenRequestValidator()
        {
            RuleFor(x => x.RefreshToken)
                .NotEmpty().WithMessage("Refresh token is required.");
        }
    }
}