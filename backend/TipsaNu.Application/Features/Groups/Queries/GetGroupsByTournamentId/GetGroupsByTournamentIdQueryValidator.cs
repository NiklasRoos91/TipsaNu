using FluentValidation;

namespace TipsaNu.Application.Features.Groups.Queries.GetGroupsByTournamentID
{
    public class GetGroupsByTournamentIdQueryValidator : AbstractValidator<GetGroupsByTournamentIdQuery>
    {
        public GetGroupsByTournamentIdQueryValidator()
        {
            RuleFor(x => x.TournamentId)
                .GreaterThan(0)
                .WithMessage("TournamentId must be a positive integer.");
        }
    }
}
