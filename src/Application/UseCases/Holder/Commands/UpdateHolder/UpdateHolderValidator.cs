using FluentValidation;

namespace Application.UseCases.Holder.Commands.UpdateHolder;

public class UpdateHolderValidator : AbstractValidator<UpdateHolderCommand>
{
    public UpdateHolderValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3)
            .MaximumLength(100);
    }
}