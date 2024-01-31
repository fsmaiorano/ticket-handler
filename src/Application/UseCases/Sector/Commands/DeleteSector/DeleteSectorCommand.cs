using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.User.Commands.SectorUser;

public record DeleteSectorCommand : IRequest<Guid?>
{
    public Guid Id { get; set; }
}

public class SectorUserHandler(ILogger<SectorUserHandler> logger, IDataContext context) : IRequestHandler<DeleteSectorCommand, Guid?>
{
    private readonly IDataContext _context = context;
    private readonly ILogger<SectorUserHandler> _logger = logger;

    public async Task<Guid?> Handle(DeleteSectorCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("DeleteSectorCommand: {@Request}", request);

            var Sector = await _context.Sectors.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (Sector is null)
            {
                _logger.LogWarning("DeleteSectorCommand: Sector not found");
                return null;
            }

            _context.Sectors.Remove(Sector);
            await _context.SaveChangesAsync(cancellationToken);

            return Sector.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "DeleteSectorCommand: {@Request}", request);
            throw;
        }
    }
}
