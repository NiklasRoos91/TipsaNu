using FluentValidation;

namespace TipsaNu.Application.Features.Matches.DTOs.Validators
{
    public class CreateMyPredictionRequestDtoValidator : AbstractValidator<CreateMyPredictionRequestDto>
    {
        public CreateMyPredictionRequestDtoValidator()
        {
            RuleFor(x => x.PredictedHomeScore).GreaterThanOrEqualTo(0);
            RuleFor(x => x.PredictedAwayScore).GreaterThanOrEqualTo(0);
        }
    }
}