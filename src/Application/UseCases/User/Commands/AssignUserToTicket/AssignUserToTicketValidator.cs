using Application.UseCases.User.Commands.AssignUserToTicket;
using FluentValidation;

namespace Application.UseCases.User.Commands.AssignUserToTicketValidator;
public class AssignUserToTicketValidatorValidator : AbstractValidator<AssignUserToTicketCommand>
{
    public AssignUserToTicketValidatorValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.TicketId)
            .NotEmpty()
            .NotNull();
    }
}