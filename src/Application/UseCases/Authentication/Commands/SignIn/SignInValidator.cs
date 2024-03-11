using FluentValidation;

namespace Application.UseCases.Authentication.Commands.SignIn;

public class SignInValidator : AbstractValidator<SignInCommand>
{
    public SignInValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .NotNull()
            .EmailAddress()
            .MinimumLength(3)
            .MaximumLength(100);

        RuleFor(x => x.Password)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3)
            .MaximumLength(100);
    }
}