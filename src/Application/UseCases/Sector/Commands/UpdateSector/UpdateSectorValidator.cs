using FluentValidation;

namespace Application.UseCases.Sector.Commands.UpdateSector;

public class UpdateSectorValidator : AbstractValidator<UpdateSectorCommand>
{
    public UpdateSectorValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3)
            .MaximumLength(100);

        RuleFor(x => x.HolderId)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.Id)
            .NotEmpty()
            .NotNull();
    }
}