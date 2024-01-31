using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.User.Commands.UpdateUser;

public record UpdateUserCommand : IRequest<Guid?>
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? Username { get; set; }
}

public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, Guid?>
{
    private readonly IDataContext _context;
    private readonly ILogger<UpdateUserHandler> _logger;

    public UpdateUserHandler(ILogger<UpdateUserHandler> logger, IDataContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<Guid?> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("UpdateUserCommand: {@Request}", request);

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (user is null)
            {
                _logger.LogWarning("UpdateUserCommand: User not found");
                return null;
            }

            if (!string.IsNullOrWhiteSpace(request.Name))
                user.Name = request.Name;

            if (!string.IsNullOrWhiteSpace(request.Email))
                user.Email = request.Email;

            if (!string.IsNullOrWhiteSpace(request.Password))
                user.Password = request.Password;

            if (!string.IsNullOrWhiteSpace(request.Username))
                user.Username = request.Username;

            _context.Users.Update(user);
            await _context.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "UpdateUserCommand: {@Request}", request);
            throw;
        }
    }
}
