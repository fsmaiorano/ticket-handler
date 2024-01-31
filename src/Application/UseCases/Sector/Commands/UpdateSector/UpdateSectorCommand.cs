using Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Sector.Commands.UpdateSector;

public record UpdateSectorCommand : IRequest<Guid?>
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public Guid HolderId { get; set; }
}

public class UpdateSectorHandler(ILogger<UpdateSectorHandler> logger, IDataContext context) : IRequestHandler<UpdateSectorCommand, Guid?>
{
    private readonly IDataContext _context = context;
    private readonly ILogger<UpdateSectorHandler> _logger = logger;

    public async Task<Guid?> Handle(UpdateSectorCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("UpdateSectorCommand: {@Request}", request);

            var Sector = await _context.Sectors.FindAsync(new object[] { request.Id }, cancellationToken);

            if (Sector is null)
            {
                _logger.LogWarning("UpdateSectorCommand: Sector not found");
                return null;
            }

            if (!string.IsNullOrWhiteSpace(request.Name))
                Sector.Name = request.Name;

            _context.Sectors.Update(Sector);
            await _context.SaveChangesAsync(cancellationToken);

            return Sector.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "UpdateSectorCommand: {@Request}", request);
            throw;
        }
    }
}
