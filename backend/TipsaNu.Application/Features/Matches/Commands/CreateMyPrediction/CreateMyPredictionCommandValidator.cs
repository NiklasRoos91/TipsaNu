using FluentValidation;

namespace TipsaNu.Application.Features.Matches.Commands.CreateMyPrediction
{
    public class CreateMyPredictionCommandValidator : AbstractValidator<CreateMyPredictionCommand>
    {
        public CreateMyPredictionCommandValidator()
        {
            RuleFor(x => x.MatchId).GreaterThan(0);

            RuleFor(x => x.Prediction)
                .NotNull();

            When(x => x.Prediction != null, () =>
            {
                RuleFor(x => x.Prediction.PredictedHomeScore)
                    .GreaterThanOrEqualTo(0);

                RuleFor(x => x.Prediction.PredictedAwayScore)
                    .GreaterThanOrEqualTo(0);
            });
        }
    }
}
