using Application.UseCases.SignUp.Commands.CreateHolderAndUser;
using FluentValidation;

namespace Application.UseCases.User.Commands.CreateHolderAndUser;

public class CreateHolderAndUserValidator : AbstractValidator<CreateHolderAndUserCommand>
{
    public CreateHolderAndUserValidator()
    {
        RuleFor(x => x.HolderName)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3)
            .MaximumLength(100);

        RuleFor(x => x.UserName)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3)
            .MaximumLength(100);

        RuleFor(x => x.UserEmail)
            .NotEmpty()
            .NotNull()
            .EmailAddress()
            .MinimumLength(3)
            .MaximumLength(100);

        RuleFor(x => x.UserPassword)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3)
            .MaximumLength(100);

        RuleFor(x => x.Username)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3)
            .MaximumLength(100);
    }
}