using FluentValidation;

namespace TipsaNu.Application.Features.Matches.Queries.GetMatchesByTournamentId
{
    public class GetMatchesByTournamentIdQueryValidator : AbstractValidator<GetMatchesByTournamentIdQuery>
    {
        public GetMatchesByTournamentIdQueryValidator()
        {
            RuleFor(x => x.TournamentId)
                .GreaterThan(0)
                .WithMessage("TournamentId must be a positive integer.");
        }
    }
}
