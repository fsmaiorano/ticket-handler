using FluentValidation;

namespace Application.UseCases.Ticket.Commands.CreateTicket;

public class CreateTicketValidator : AbstractValidator<CreateTicketCommand>
{
    public CreateTicketValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3)
            .MaximumLength(100);

        RuleFor(x => x.Content)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3)
            .MaximumLength(1000);

        RuleFor(x => x.Status)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.Priority)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.HolderId)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.SectorId)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.AssigneeId)
            .NotEmpty()
            .NotNull();
    }
}


