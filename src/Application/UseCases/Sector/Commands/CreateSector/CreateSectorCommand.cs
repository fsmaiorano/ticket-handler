using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Sector.Commands.CreateSector;

public record CreateSectorCommand : IRequest<Guid?>
{
    public string Name { get; init; } = string.Empty;
    public Guid HolderId { get; init; }
    public List<Guid> Users { get; init; } = [];
}

public class CreateSectorHandler(ILogger<CreateSectorHandler> logger, IDataContext context) : IRequestHandler<CreateSectorCommand, Guid?>
{
    private readonly IDataContext _context = context;
    private readonly ILogger<CreateSectorHandler> _logger = logger;

    public async Task<Guid?> Handle(CreateSectorCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("CreateSectorCommand: {@Request}", request);

            var sector = new SectorEntity
            {
                Name = request.Name,
                HolderId = request.HolderId,
            };

            _context.Sectors.Add(sector);
            await _context.SaveChangesAsync(cancellationToken);

            var createdSector = await _context.Sectors.FindAsync([sector.Id], cancellationToken);

            return createdSector?.Id ?? null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "CreateSectorCommand: {@Request}", request);
            throw;
        }
    }
}