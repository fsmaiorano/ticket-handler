using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.User.Commands.CreateUser;

public record CreateUserCommand : IRequest<Guid?>
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string Username { get; set; }
}

public class CreateUserHandler(ILogger<CreateUserHandler> logger, IDataContext context) : IRequestHandler<CreateUserCommand, Guid?>
{
    private readonly IDataContext _context = context;
    private readonly ILogger<CreateUserHandler> _logger = logger;

    public async Task<Guid?> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CreateUserCommand: {@Request}", request);

        var user = new UserEntity
        {
            Name = request.Name,
            Email = request.Email,
            Password = request.Password,
            Username = request.Username
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);

        var createdUser = await _context.Users.FindAsync([user.Id], cancellationToken);

        return createdUser?.Id ?? null;
    }
}
