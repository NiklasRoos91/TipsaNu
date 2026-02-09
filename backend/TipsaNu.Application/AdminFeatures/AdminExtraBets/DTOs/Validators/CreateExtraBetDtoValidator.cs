using FluentValidation;

namespace TipsaNu.Application.AdminFeatures.AdminExtraBets.DTOs.Validators
{
    public class CreateExtraBetDtoValidator : AbstractValidator<CreateExtraBetOptionDto>
    {
        public CreateExtraBetDtoValidator()
        {
            RuleFor(x => x.TournamentId)
                .GreaterThan(0).WithMessage("TournamentId must be greater than 0");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required")
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters");

            RuleFor(x => x.Points)
                .GreaterThanOrEqualTo(0).WithMessage("Points must be >= 0");

            RuleFor(x => x.ExpiresAt)
                .Must(BeAValidDeadline).WithMessage("ExpiresAt must be in the future")
                .When(x => x.ExpiresAt.HasValue);

            RuleForEach(x => x.Choices)
                .NotEmpty().WithMessage("Choice cannot be empty")
                .MaximumLength(100).WithMessage("Choice cannot exceed 100 characters");

            When(x => !x.AllowCustomChoice, () =>
            {
                RuleFor(x => x.Choices)
                    .NotEmpty().WithMessage("At least one choice must be provided when custom choices are not allowed");

                RuleForEach(x => x.Choices)
                    .NotEmpty().WithMessage("Choice cannot be empty")
                    .MaximumLength(100).WithMessage("Choice cannot exceed 100 characters");
            });
        }

        private bool BeAValidDeadline(DateTime? deadline)
        {
            if (!deadline.HasValue) return true;
            return deadline.Value > DateTime.UtcNow;
        }
    }
}
