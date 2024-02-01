using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.User.Queries;

public record GetUserByIdQuery : IRequest<UserEntity?>
{
    public required Guid Id { get; init; }
}

public class GetUserByIdHandler(ILogger<GetUserByIdHandler> logger, IDataContext context) : IRequestHandler<GetUserByIdQuery, UserEntity?>
{
    private readonly IDataContext _context = context;
    private readonly ILogger<GetUserByIdHandler> _logger = logger;

    public async Task<UserEntity?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        UserEntity? user = default;

        try
        {
            _logger.LogInformation("GetUserById: {@Request}", request);

            user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetUserById: {@Request}", request);
        }

        return user;
    }
}
