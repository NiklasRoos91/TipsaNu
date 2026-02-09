using FluentValidation;
using TipsaNu.Domain.Interfaces;

namespace TipsaNu.Application.Features.ExtraBets.Commands.CreateExtraBet
{
    public class CreateExtraBetCommandValidator : AbstractValidator<CreateExtraBetCommand>
    {
        private readonly IExtraBetRepository _repository;

        public CreateExtraBetCommandValidator(IExtraBetRepository repository)
        {
            _repository = repository;

            RuleFor(x => x.OptionId)
                .GreaterThan(0).WithMessage("OptionId is required");

            RuleFor(x => x.CreateExtraBetDto.Value)
                .NotEmpty().WithMessage("Value is required");

            RuleFor(x => x)
                .MustAsync(async (cmd, ct) =>
                {
                    var option = await _repository
                        .GetOptionByIdWithChoicesAsync(cmd.OptionId, ct);

                    if (option == null)
                        return false;

                    if (!option.AllowCustomChoice)
                    {
                        return option.ExtraBetOptionChoices
                            .Any(c => c.Value.Trim().ToLower() ==
                                      cmd.CreateExtraBetDto.Value.Trim().ToLower());
                    }

                    return true;
                })
                .WithMessage("Invalid value for this ExtraBetOption");
        }
    }
}