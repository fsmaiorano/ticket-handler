using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Sector.Queries;

public record GetSectorsByHolderIdQuery : IRequest<List<SectorEntity>?>
{
    public required Guid HolderId { get; init; }
}

public class GetSectorsByHolderIdHandler(ILogger<GetSectorsByHolderIdHandler> logger, IDataContext context) : IRequestHandler<GetSectorsByHolderIdQuery, List<SectorEntity>?>
{
    private readonly IDataContext _context = context;
    private readonly ILogger<GetSectorsByHolderIdHandler> _logger = logger;

    public async Task<List<SectorEntity>?> Handle(GetSectorsByHolderIdQuery request, CancellationToken cancellationToken)
    {
        List<SectorEntity>? sectors;

        try
        {
            _logger.LogInformation("GetSectorsByHolderIdQuery.Handle");

            sectors = await _context.Sectors.Where(x => x.HolderId == request.HolderId).ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetSectorsByHolderIdQuery.Handle");
            sectors = default;
        }

        return sectors;
    }
}

