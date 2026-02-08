using FluentValidation;
using TipsaNu.Application.Features.Leagues.DTOs;

namespace TipsaNu.Application.Features.Leagues.Commands.CreateLeague
{
    public class CreateLeagueCommandValidator : AbstractValidator<CreateLeagueCommand>
    {
        public CreateLeagueCommandValidator(IValidator<CreateLeagueDto> leagueDtoValidator)
        {
            RuleFor(x => x.LeagueDto)
                .SetValidator(leagueDtoValidator);
        }
    }
}
