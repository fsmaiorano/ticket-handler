using FluentValidation;

namespace Application.UseCases.Ticket.Commands.DeleteTicket;

public class DeleteTicketValidator : AbstractValidator<DeleteTicketCommand>
{
    public DeleteTicketValidator()
    {
        RuleFor(x => x.Id)
               .NotEmpty()
               .NotNull();
    }
}
