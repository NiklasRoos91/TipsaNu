using FluentValidation;
using TipsaNu.Application.AdminFeatures.AdminExtraBets.DTOs.Validators;

namespace TipsaNu.Application.AdminFeatures.AdminExtraBets.Commands.SetExtraBetOptionCorrectValues
{
    public class SetExtraBetOptionCorrectValuesCommandValidator
        : AbstractValidator<SetExtraBetOptionCorrectValuesCommand>
    {
        public SetExtraBetOptionCorrectValuesCommandValidator()
        {
            RuleFor(x => x.SetExtraBetOptionCorrectValuesDto)
                .NotNull()
                .SetValidator(new SetExtraBetOptionCorrectValuesDtoValidator());
        }
    }
}
