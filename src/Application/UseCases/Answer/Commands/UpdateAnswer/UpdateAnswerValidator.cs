using FluentValidation;

namespace Application.UseCases.Answer.Commands.UpdateAnswer;

public class UpdateAnswerValidator : AbstractValidator<UpdateAnswerCommand>
{
    public UpdateAnswerValidator()
    {
        RuleFor(x => x.Content)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3)
            .MaximumLength(1000);

        RuleFor(x => x.TicketId)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.UserId)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.HolderId)
            .NotEmpty()
            .NotNull();

        RuleFor(x => x.SectorId)
            .NotEmpty()
            .NotNull();
    }
}
