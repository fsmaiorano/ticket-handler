using FluentValidation;

namespace Application.UseCases.Authentication.Commands.SignUp;

public class SignUpValidator : AbstractValidator<SignUpCommand>
{
    public SignUpValidator()
    {
        RuleFor(x => x.HolderName)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3)
            .MaximumLength(100);

        RuleFor(x => x.FullName)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3)
            .MaximumLength(100);

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