using FluentValidation;

namespace TipsaNu.Application.Features.Leagues.DTOs.Validators
{
    public class CreateLeagueDtoValidator : AbstractValidator<CreateLeagueDto>
    {
        public CreateLeagueDtoValidator()
        {
            RuleFor(x => x.TournamentId)
                .GreaterThan(0).WithMessage("TournamentId must be greater than 0");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("League name is required")
                .MaximumLength(50).WithMessage("League name cannot exceed 50 characters");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("League description is required")
                .MaximumLength(200).WithMessage("League description cannot exceed 200 characters");

            RuleFor(x => x.MaxMembers)
                .GreaterThan(0).WithMessage("MaxMembers must be specified and greater than 0");
        }
    }
}
