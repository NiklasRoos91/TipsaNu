using FluentValidation;

namespace TipsaNu.Application.Features.ExtraBets.Queries.GetExtraBetOptions
{
    public class GetExtraBetOptionsQueryValidator
        : AbstractValidator<GetExtraBetOptionsQuery>
    {
        private static readonly string[] AllowedStatuses = { "all", "open", "closed" };

        public GetExtraBetOptionsQueryValidator()
        {
            RuleFor(x => x.TournamentId)
                .GreaterThan(0)
                .WithMessage("TournamentId must be greater than 0.");

            RuleFor(x => x.Status)
                .NotEmpty()
                .WithMessage("Status is required.")
                .Must(s => AllowedStatuses.Contains(s.ToLower()))
                .WithMessage($"Status must be one of: {string.Join(", ", AllowedStatuses)}.");
        }
    }
}
