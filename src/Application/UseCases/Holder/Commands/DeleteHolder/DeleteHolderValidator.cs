using FluentValidation;

namespace Application.UseCases.Holder.Commands.DeleteHolder;

public class DeleteHolderValidator : AbstractValidator<DeleteHolderCommand>
{
    public DeleteHolderValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .NotNull();
    }
}
