using FluentValidation;

namespace TipsaNu.Application.AdminFeatures.AdminExtraBets.DTOs.Validators
{
    public class SetExtraBetOptionCorrectValuesDtoValidator
        : AbstractValidator<SetExtraBetOptionCorrectValuesDto>
    {
        public SetExtraBetOptionCorrectValuesDtoValidator()
        {
            RuleFor(x => x.CorrectValues)
                .NotEmpty().WithMessage("At least one correct value must be provided.");
        }
    }
}
