using FluentValidation;
using TipsaNu.Application.AdminFeatures.AdminExtraBets.DTOs.Validators;

namespace TipsaNu.Application.AdminFeatures.AdminExtraBets.Commands.AddExtraBetOptionCorrectValues
{
    public class AddExtraBetOptionCorrectValuesCommandValidator
    : AbstractValidator<AddExtraBetOptionCorrectValuesCommand>
    {
        public AddExtraBetOptionCorrectValuesCommandValidator()
        {
            RuleFor(x => x.SetExtraBetOptionCorrectValuesDto)
                .NotNull()
                .SetValidator(new SetExtraBetOptionCorrectValuesDtoValidator());
        }
    }
}