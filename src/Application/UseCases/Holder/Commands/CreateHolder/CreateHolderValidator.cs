using FluentValidation;

namespace Application.UseCases.Holder.Commands.CreateHolder;

public class CreateHolderValidator : AbstractValidator<CreateHolderCommand>
{
    public CreateHolderValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3)
            .MaximumLength(100);
    }
}