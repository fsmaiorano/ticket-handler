using Application.UseCases.User.Commands.AssignUserToSector;
using FluentValidation;

namespace Application.UseCases.User.Commands.AssignUserToSectorValidator;
public class AssignUserToSectorValidatorValidator : AbstractValidator<AssignUserToSectorCommand>
{
    public AssignUserToSectorValidatorValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.SectorsId)
            .NotEmpty()
            .NotNull();
    }
}