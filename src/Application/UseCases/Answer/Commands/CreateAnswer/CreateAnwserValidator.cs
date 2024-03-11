using FluentValidation;

namespace Application.UseCases.Answer.Commands.CreateAnswer;

public class CreateAnwserValidator : AbstractValidator<CreateAnswerCommand>
{
    public CreateAnwserValidator()
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
