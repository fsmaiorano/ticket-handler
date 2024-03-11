using FluentValidation;

namespace Application.UseCases.Answer.Commands.DeleteAnswer;

public class DeleteAnwserValidator : AbstractValidator<DeleteAnswerCommand>
{
    public DeleteAnwserValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .NotNull();
    }
}
