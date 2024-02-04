using Application.Common.Interfaces;
using Domain.Constants;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.SignUp.Commands.CreateHolderAndUser;

public record CreateHolderAndUserCommand : IRequest<CreateHolderAndUserResponse>
{
    public required string HolderName { get; set; }
    public required string UserName { get; set; }
    public required string UserEmail { get; set; }
    public required string UserPassword { get; set; }
    public required string Username { get; set; }
}

public record CreateHolderAndUserResponse
{
    public Guid CreatedUser { get; init; }
    public Guid CreatedHolder { get; init; }
}

public class CreateHolderAndUserHandler(ILogger<CreateHolderAndUserHandler> logger, IDataContext context) : IRequestHandler<CreateHolderAndUserCommand, CreateHolderAndUserResponse>
{
    private readonly IDataContext _context = context;
    private readonly ILogger<CreateHolderAndUserHandler> _logger = logger;

    public async Task<CreateHolderAndUserResponse> Handle(CreateHolderAndUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("CreateHolderAndUserCommand: {@Request}", request);

            var holder = new HolderEntity
            {
                Name = request.HolderName
            };

            var user = new UserEntity
            {
                Name = request.UserName,
                Email = request.UserEmail,
                Password = request.UserPassword,
                Username = request.Username,
                Role = UserRoles.Administrator,
                Holder = holder,
                HolderId = holder.Id
            };

            _context.Holders.Add(holder);
            _context.Users.Add(user);

            await _context.SaveChangesAsync(cancellationToken);
            
            return new CreateHolderAndUserResponse
            {
                CreatedUser = user.Id,
                CreatedHolder = holder.Id
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "CreateHolderAndUserCommand: {@Request}", request);
            throw;
        }
    }
}
