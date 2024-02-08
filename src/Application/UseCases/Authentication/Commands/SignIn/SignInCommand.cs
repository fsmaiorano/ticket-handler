using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Authentication.Commands.SignIn;

public record SignInCommand : IRequest<SignInResponse>
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public class SignInResponse : BaseResponse
{
    public string? RedirectUrl { get; set; }
    public string? Token { get; set; }
}

public class SignInCommandHandler(ILogger<SignInCommandHandler> logger, IDataContext context) : IRequestHandler<SignInCommand, SignInResponse>
{
    private readonly IDataContext _context = context;
    private readonly ILogger<SignInCommandHandler> _logger = logger;

    public async Task<SignInResponse> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var response = new SignInResponse();

        try
        {
            _logger.LogInformation("SignInCommand: {@Request}", request);

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);

            if (user is null)
            {
                _logger.LogWarning("SignInCommand: User not found");
                response.Message = "User not found";

                return response;
            }


            if (!user.Password.Equals(request.Password))
            {
                _logger.LogWarning("SignInCommand: Invalid password");
                response.Message = "Invalid password";

                return response;
            }

            // Implement JWT token generation here
            response.Success = true;
            response.Message = "User signed in";
            response.RedirectUrl = "/";
            response.Token = "JWT Token";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "SignInCommand: {@Request}", request);
            response.Message = "Error signing in";
        }

        return response;
    }
}

