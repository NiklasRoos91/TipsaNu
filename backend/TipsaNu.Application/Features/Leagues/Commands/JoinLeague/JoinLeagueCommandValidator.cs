using FluentValidation;
using TipsaNu.Application.Features.Leagues.DTOs;

namespace TipsaNu.Application.Features.Leagues.Commands.JoinLeague
{
    public class JoinLeagueCommandValidator : AbstractValidator<JoinLeagueCommand>
    {
        public JoinLeagueCommandValidator(IValidator<JoinLeagueDto> dtoValidator)
        {
            RuleFor(x => x.TournamentId)
                .GreaterThan(0)
                .WithMessage("TournamentId must be greater than 0.");

            RuleFor(x => x.Dto)
                .NotNull()
                .WithMessage("Body is required.")
                .SetValidator(dtoValidator);
        }
    }
}