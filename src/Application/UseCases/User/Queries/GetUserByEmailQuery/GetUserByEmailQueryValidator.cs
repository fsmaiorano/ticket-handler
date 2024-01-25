using FluentValidation;

namespace Application.UseCases.User.Queries.GetUserByEmailQuery;

public class GetUserByEmailQueryValidator : AbstractValidator<GetUserByEmail>
{
    public GetUserByEmailQueryValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MinimumLength(3)
            .MaximumLength(100);
    }
}
