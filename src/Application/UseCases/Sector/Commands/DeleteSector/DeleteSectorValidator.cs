using Application.UseCases.User.Commands.SectorUser;
using FluentValidation;

namespace Application.UseCases.Sector.Commands.DeleteSector;

public class DeleteSectorValidator : AbstractValidator<DeleteSectorCommand>
{
    public DeleteSectorValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .NotNull();
    }
}
