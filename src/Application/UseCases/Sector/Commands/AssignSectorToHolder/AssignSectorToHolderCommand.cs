using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Sector.Commands.AssignSectorToHolder;
public record AssignSectorToHolderCommand : IRequest<bool>
{
    public required Guid HolderId { get; set; }
    public required List<Guid> SectorsId { get; set; }
}

public class AssignSectorToHolderHandler(ILogger<AssignSectorToHolderHandler> logger, IDataContext context) : IRequestHandler<AssignSectorToHolderCommand, bool>
{
    private readonly IDataContext _context = context;
    private readonly ILogger<AssignSectorToHolderHandler> _logger = logger;

    public async Task<bool> Handle(AssignSectorToHolderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("AssignSectorToHolderCommand: {@Request}", request);

            var holder = await _context.Holders.FindAsync([request.HolderId], cancellationToken);

            if (holder is null)
            {
                _logger.LogWarning("AssignSectorToHolderCommand: Holder not found");
                return false;
            }

            var sectors = await _context.Sectors
                                .Where(s => request.SectorsId.Contains(s.Id))
                                .ToListAsync(cancellationToken);

            if (sectors.Count > 0)
            {
                var sectorsNotAssigned = sectors.Where(s => holder.Sectors is not null &&
                                                            !holder.Sectors
                                                                .Any(us => us.Id == s.Id))
                                                                .ToList();

                if (sectorsNotAssigned.Count > 0 && holder.Sectors != null)
                {
                    holder.Sectors.AddRange(sectorsNotAssigned);
                    await _context.SaveChangesAsync(cancellationToken);
                }
            }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "AssignSectorToHolderCommand: {@Request}", request);
            throw;
        }
    }
}