using FluentValidation;

namespace TipsaNu.Application.Features.Tournaments.Queries.GetById
{
    public class GetTournamentByIdQueryValidator : AbstractValidator<GetTournamentByIdQuery>
    {
        public GetTournamentByIdQueryValidator()
        {
            RuleFor(x => x.TournamentId)
                .GreaterThan(0)
                .WithMessage("TournamentId must be a positive integer.");
        }
    }
}