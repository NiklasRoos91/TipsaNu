using FluentValidation;
using TipsaNu.Application.AdminFeatures.AdminExtraBets.DTOs.Validators;

namespace TipsaNu.Application.AdminFeatures.AdminExtraBets.Commands.CreateExtraBetOptionCorrectValues
{
    public class CreateExtraBetOptionCorrectValuesCommandValidator
        : AbstractValidator<CreateExtraBetOptionCorrectValuesCommand>
    {
        public CreateExtraBetOptionCorrectValuesCommandValidator()
        {
            RuleFor(x => x.SetExtraBetOptionCorrectValuesDto)
                .NotNull()
                .SetValidator(new SetExtraBetOptionCorrectValuesDtoValidator());
        }
    }
}
