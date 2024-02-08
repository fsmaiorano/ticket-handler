using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Sector.Commands.CreateSector;

public record CreateSectorCommand : IRequest<CreateSectorResponse>
{
    public string Name { get; init; } = string.Empty;
    public Guid HolderId { get; init; }
    public List<Guid> Users { get; init; } = [];
}

public class CreateSectorResponse : BaseResponse
{
    public SectorEntity? Sector { get; set; }
}

public class CreateSectorHandler(ILogger<CreateSectorHandler> logger, IDataContext context) : IRequestHandler<CreateSectorCommand, CreateSectorResponse>
{
    private readonly IDataContext _context = context;
    private readonly ILogger<CreateSectorHandler> _logger = logger;

    public async Task<CreateSectorResponse> Handle(CreateSectorCommand request, CancellationToken cancellationToken)
    {
        var response = new CreateSectorResponse();

        try
        {
            _logger.LogInformation("CreateSectorCommand: {@Request}", request);

            var sector = new SectorEntity
            {
                Name = request.Name,
                HolderId = request.HolderId,
            };

            var sectorExists = await _context.Sectors.AnyAsync(x => x.Name == request.Name, cancellationToken);

            if (sectorExists)
            {
                _logger.LogWarning("CreateSectorCommand: Sector already exists");
                response.Message = "Sector already exists";

                return response;
            }

            _context.Sectors.Add(sector);
            await _context.SaveChangesAsync(cancellationToken);

            var createdSector = await _context.Sectors.FindAsync([sector.Id], cancellationToken);

            if (createdSector is null)
            {
                _logger.LogWarning("CreateSectorCommand: Sector not found");
                response.Message = "Sector not found";

                return response;
            }

            response.Success = true;
            response.Message = "Sector created";
            response.Sector = createdSector;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "CreateSectorCommand: {@Request}", request);
            response.Message = ex.Message;
        }

        return response;
    }
}