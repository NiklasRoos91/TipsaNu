using FluentValidation;

namespace TipsaNu.Application.Features.Matches.Commands.CreatePrediction
{
    public class CreatePredictionCommandValidator : AbstractValidator<CreatePredictionCommand>
    {
        public CreatePredictionCommandValidator()
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
