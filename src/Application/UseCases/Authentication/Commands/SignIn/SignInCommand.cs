using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Authentication.Commands.SignIn;

public record SignInCommand : IRequest<string?>
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public class SignInCommandHandler(ILogger<SignInCommandHandler> logger, IDataContext context) : IRequestHandler<SignInCommand, string?>
{
    private readonly IDataContext _context = context;
    private readonly ILogger<SignInCommandHandler> _logger = logger;

    public async Task<string?> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("SignInCommand: {@Request}", request);

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);

            if (user is null)
                return null;

            if (!user.Password.Equals(request.Password))
                return null;

            // Implement JWT token generation here
            return user.Id.ToString();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "SignInCommand: {@Request}", request);
            throw;
        }
    }
}

