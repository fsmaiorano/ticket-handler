using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.User.Commands.UpdateUser;

public record UpdateUserCommand : IRequest<UpdateUserResponse>
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
}

public class UpdateUserResponse : BaseResponse
{

}

public class UpdateUserHandler(ILogger<UpdateUserHandler> logger, IDataContext context) : IRequestHandler<UpdateUserCommand, UpdateUserResponse>
{
    private readonly IDataContext _context = context;
    private readonly ILogger<UpdateUserHandler> _logger = logger;

    public async Task<UpdateUserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var response = new UpdateUserResponse();

        try
        {
            _logger.LogInformation("UpdateUserCommand: {@Request}", request);

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (user is null)
            {
                _logger.LogWarning("UpdateUserCommand: User not found");
                response.Message = "User not found";

                return response;
            }

            if (!string.IsNullOrWhiteSpace(request.Name))
                user.Name = request.Name;

            if (!string.IsNullOrWhiteSpace(request.Email))
                user.Email = request.Email;

            if (!string.IsNullOrWhiteSpace(request.Password))
                user.Password = request.Password;

            _context.Users.Update(user);
            await _context.SaveChangesAsync(cancellationToken);

            response.Success = true;
            response.Message = "User updated";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "UpdateUserCommand: {@Request}", request);
            response.Message = ex.Message;
        }

        return response;
    }
}
