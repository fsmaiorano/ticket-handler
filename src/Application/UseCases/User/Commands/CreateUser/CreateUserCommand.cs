using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Constants;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.User.Commands.CreateUser;

public record CreateUserCommand : IRequest<CreateUserResponse>
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required Guid HolderId { get; set; }
    public required UserRoles Role { get; set; }
    public List<Guid>? SectorsId { get; set; }
}

public class CreateUserResponse : BaseResponse
{
    public UserEntity? User { get; set; }
}

public class CreateUserHandler(ILogger<CreateUserHandler> logger, IDataContext context) : IRequestHandler<CreateUserCommand, CreateUserResponse>
{
    private readonly IDataContext _context = context;
    private readonly ILogger<CreateUserHandler> _logger = logger;

    public async Task<CreateUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var response = new CreateUserResponse();

        try
        {
            _logger.LogInformation("CreateUserCommand: {@Request}", request);

            var user = new UserEntity
            {
                Name = request.Name,
                Email = request.Email,
                Password = request.Password,
                Role = UserRoles.Administrator,
                HolderId = request.HolderId,
            };

            var userExists = await _context.Users.AnyAsync(x => x.Email == request.Email, cancellationToken);

            if (userExists)
            {
                _logger.LogWarning("CreateUserCommand: User already exists");
                response.Message = "User already exists";

                return response;
            }

            if (request.SectorsId is not null)
            {
                user.Sectors = [];

                foreach (var sectorId in request.SectorsId)
                {
                    var sector = await _context.Sectors.FindAsync([sectorId], cancellationToken);

                    if (sector is not null)
                        user.Sectors.Add(sector);
                }
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            var createdUser = await _context.Users.FindAsync([user.Id], cancellationToken);

            if (createdUser is null)
            {
                _logger.LogWarning("CreateUserCommand: User not found");
                response.Message = "User not found";

                return response;
            }

            response.Success = true;
            response.Message = "User created";
            response.User = createdUser;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "CreateUserCommand: {@Request}", request);
            response.Message = ex.Message;
        }

        return response;
    }
}
