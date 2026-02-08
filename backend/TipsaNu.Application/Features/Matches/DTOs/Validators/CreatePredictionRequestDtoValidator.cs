using FluentValidation;

namespace TipsaNu.Application.Features.Matches.DTOs.Validators
{
    public class CreatePredictionRequestDtoValidator : AbstractValidator<CreatePredictionRequestDto>
    {
        public CreatePredictionRequestDtoValidator()
        {
            RuleFor(x => x.PredictedHomeScore).GreaterThanOrEqualTo(0);
            RuleFor(x => x.PredictedAwayScore).GreaterThanOrEqualTo(0);
        }
    }
}