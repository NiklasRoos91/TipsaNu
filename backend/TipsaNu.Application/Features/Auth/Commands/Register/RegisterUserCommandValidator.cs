using FluentValidation;
using TipsaNu.Application.Commons.Validators;
using TipsaNu.Application.Feature.Auth.Commands.Register;

namespace TipsaNu.Application.Features.Auth.Commands.Register
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.Request).NotNull().WithMessage("Request object is required.");

            RuleFor(x => x.Request.Username)
                .NotEmpty().WithMessage("Username is required.")
                .MaximumLength(100);

            RuleFor(x => x.Request.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Request.Password)
                .ApplyPasswordRules();
        }
    }
}