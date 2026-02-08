using FluentValidation;

namespace TipsaNu.Application.Features.Predictions.Queries.GetMyPredictionForMatch
{
    public class GetMyPredictionForMatchQueryValidator
        : AbstractValidator<GetMyPredictionForMatchQuery>
    {
        public GetMyPredictionForMatchQueryValidator()
        {
            RuleFor(x => x.MatchId).GreaterThan(0).WithMessage("MatchId must be a positive integer.");
        }
    }
}
