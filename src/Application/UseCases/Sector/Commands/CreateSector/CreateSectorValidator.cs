using FluentValidation;

namespace Application.UseCases.Sector.Commands.CreateSector;

public class CreateSectorValidator : AbstractValidator<CreateSectorCommand>
{
    public CreateSectorValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3)
            .MaximumLength(100);

        RuleFor(x => x.HolderId)
            .NotEmpty()
            .NotNull();
    }
}