using Application.Common.Interfaces;
using Application.UseCases.Sector.Commands.CreateSector;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Sector.Commands.AssignUserToSector.Commands;
public record AssignUserToSectorCommand : IRequest<bool>
{
    public required Guid UserId { get; set; }
    public required List<Guid> SectorsId { get; set; }
}

public class AssignUserToSectorHandler(ILogger<AssignUserToSectorHandler> logger, IDataContext context) : IRequestHandler<AssignUserToSectorCommand, bool>
{
    private readonly IDataContext _context = context;
    private readonly ILogger<AssignUserToSectorHandler> _logger = logger;

    public async Task<bool> Handle(AssignUserToSectorCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("AssignUserToSectorCommand: {@Request}", request);

            var user = await _context.Users.FindAsync([request.UserId], cancellationToken);

            if (user is null)
            {
                _logger.LogWarning("AssignUserToSectorCommand: User not found");
                return false;
            }

            var sectors = await _context.Sectors
                                .Where(s => request.SectorsId.Contains(s.Id))
                                .ToListAsync(cancellationToken);

            if (sectors.Count > 0)
            {
                var sectorsNotAssigned = sectors.Where(s => user.Sectors is not null && 
                                                            !user.Sectors
                                                                .Any(us => us.Id == s.Id))
                                                                .ToList();

                if (sectorsNotAssigned.Count > 0 && user.Sectors != null)
                {
                    user.Sectors.AddRange(sectorsNotAssigned);
                    await _context.SaveChangesAsync(cancellationToken);
                }
            }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "AssignUserToSectorCommand: {@Request}", request);
            throw;
        }
    }
}