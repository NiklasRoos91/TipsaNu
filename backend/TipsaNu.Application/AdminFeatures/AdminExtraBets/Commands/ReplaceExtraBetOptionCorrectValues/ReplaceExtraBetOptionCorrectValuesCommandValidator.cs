using FluentValidation;
using TipsaNu.Application.AdminFeatures.AdminExtraBets.DTOs.Validators;

namespace TipsaNu.Application.AdminFeatures.AdminExtraBets.Commands.ReplaceExtraBetOptionCorrectValues
{
    public class ReplaceExtraBetOptionCorrectValuesCommandValidator
        : AbstractValidator<ReplaceExtraBetOptionCorrectValuesCommand>
    {
        public ReplaceExtraBetOptionCorrectValuesCommandValidator()
        {
            RuleFor(x => x.SetExtraBetOptionCorrectValuesDto)
                .NotNull()
                .SetValidator(new SetExtraBetOptionCorrectValuesDtoValidator());
        }
    }
}