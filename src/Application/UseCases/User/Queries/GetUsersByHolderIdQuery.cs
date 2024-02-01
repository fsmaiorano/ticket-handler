using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.User.Queries;

public record GetUsersByHolderIdQuery : IRequest<List<UserEntity>?>
{
    public required Guid HolderId { get; set; }
}

public class GetUsersByHolderIdHandler(ILogger<GetUsersByHolderIdHandler> logger, IDataContext context) : IRequestHandler<GetUsersByHolderIdQuery, List<UserEntity>?>
{
    private readonly IDataContext _context = context;
    private readonly ILogger<GetUsersByHolderIdHandler> _logger = logger;

    public async Task<List<UserEntity>?> Handle(GetUsersByHolderIdQuery request, CancellationToken cancellationToken)
    {
        List<UserEntity>? users = default;

        try
        {
            _logger.LogInformation("GetUsersByHolderId: {@Request}", request);

            users = await _context.Users.Where(x => x.HolderId == request.HolderId).ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetUsersByHolderId: {@Request}", request);
        }

        return users;
    }
}
