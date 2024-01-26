using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.User.Queries;

public record GetUserByEmail : IRequest<UserEntity?>
{
    public required string Email { get; set; }
}

public class GetUserByEmailHandler(ILogger<GetUserByEmailHandler> logger, IDataContext context) : IRequestHandler<GetUserByEmail, UserEntity?>
{
    private readonly IDataContext _context = context;
    private readonly ILogger<GetUserByEmailHandler> _logger = logger;

    public async Task<UserEntity?> Handle(GetUserByEmail request, CancellationToken cancellationToken)
    {
        UserEntity? user = default;

        try
        {
            _logger.LogInformation("GetUserByEmail: {@Request}", request);

            user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetUserByEmail: {@Request}", request);
        }

        return user;
    }
}
