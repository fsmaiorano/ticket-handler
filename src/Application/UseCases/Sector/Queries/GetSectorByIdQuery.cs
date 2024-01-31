using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Sector.Queries;

public record GetSectorByIdQuery : IRequest<SectorEntity?>
{
    public required Guid Id { get; init; }
}

public class GetSectorByIdHandler : IRequestHandler<GetSectorByIdQuery, SectorEntity?>
{
    private readonly IDataContext _context;
    private readonly ILogger<GetSectorByIdHandler> _logger;

    public GetSectorByIdHandler(ILogger<GetSectorByIdHandler> logger, IDataContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<SectorEntity?> Handle(GetSectorByIdQuery request, CancellationToken cancellationToken)
    {
        SectorEntity? Sector = default;

        try
        {
            _logger.LogInformation("GetSectorById: {@Request}", request);

            Sector = await _context.Sectors.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetSectorById: {@Request}", request);
        }

        return Sector;
    }
}
