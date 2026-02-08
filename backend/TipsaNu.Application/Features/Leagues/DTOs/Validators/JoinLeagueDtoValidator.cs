using FluentValidation;

namespace TipsaNu.Application.Features.Leagues.DTOs.Validators
{
    public class JoinLeagueDtoValidator : AbstractValidator<JoinLeagueDto>
    {
        public JoinLeagueDtoValidator()
        {
            RuleFor(x => x.InvitationCode)
                .NotEmpty()
                .WithMessage("Invitation code is required.")
                .MaximumLength(32)
                .WithMessage("Invitation code is too long.");
        }
    }
}
