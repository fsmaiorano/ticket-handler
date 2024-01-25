using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases;

public record CreateUserCommand : IRequest<Guid?>
{
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public string Username { get; init; } = string.Empty;
}

public class CreateUserHandler : IRequestHandler<CreateUserCommand, Guid?>
{
    private readonly IDataContext _context;
    private readonly ILogger<CreateUserHandler> _logger;

    public CreateUserHandler(ILogger<CreateUserHandler> logger, IDataContext context)
    {
        _logger = logger;
        _context = context;
    }

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

        var createdUser = await _context.Users.FindAsync(user.Id);

        return createdUser?.Id ?? null;
    }
}
