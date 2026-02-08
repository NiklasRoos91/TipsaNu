using FluentValidation;

namespace TipsaNu.Application.Features.Leagues.Queries.GetMyLeaguesInTournament
{
    public class GetMyLeaguesInTournamentQueryValidator
        : AbstractValidator<GetMyLeaguesInTournamentQuery>
    {
        public GetMyLeaguesInTournamentQueryValidator()
        {
            RuleFor(x => x.TournamentId)
                .GreaterThan(0)
                .WithMessage("TournamentId must be greater than 0.");
        }
    }
}
