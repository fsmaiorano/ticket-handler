using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.User.Commands.DeleteUser;

public record DeleteUserCommand : IRequest<Guid?>
{
    public Guid Id { get; set; }
}

public class DeleteUserHandler(ILogger<DeleteUserHandler> logger, IDataContext context) : IRequestHandler<DeleteUserCommand, Guid?>
{
    private readonly IDataContext _context = context;
    private readonly ILogger<DeleteUserHandler> _logger = logger;

    public async Task<Guid?> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("DeleteUserCommand: {@Request}", request);

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (user is null)
            {
                _logger.LogWarning("DeleteUserCommand: User not found");
                return null;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "DeleteUserCommand: {@Request}", request);
            throw;
        }
    }
}