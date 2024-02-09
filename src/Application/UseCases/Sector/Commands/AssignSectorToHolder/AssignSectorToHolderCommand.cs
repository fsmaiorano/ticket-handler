using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Sector.Commands.AssignSectorToHolder;
public record AssignSectorToHolderCommand : IRequest<AssignSectorToHolderResponse>
{
    public required Guid HolderId { get; set; }
    public required List<Guid> SectorsId { get; set; }
}

public class AssignSectorToHolderResponse : BaseResponse
{
    public HolderDto? Holder { get; set; }
    public List<SectorDto>? Sectors { get; set; }
}

public class AssignSectorToHolderHandler(ILogger<AssignSectorToHolderHandler> logger, IDataContext context) : IRequestHandler<AssignSectorToHolderCommand, AssignSectorToHolderResponse>
{
    private readonly IDataContext _context = context;
    private readonly ILogger<AssignSectorToHolderHandler> _logger = logger;

    public async Task<AssignSectorToHolderResponse> Handle(AssignSectorToHolderCommand request, CancellationToken cancellationToken)
    {
        var response = new AssignSectorToHolderResponse();

        try
        {
            _logger.LogInformation("AssignSectorToHolderCommand: {@Request}", request);

            var holder = await _context.Holders.FindAsync([request.HolderId], cancellationToken);

            if (holder is null)
            {
                _logger.LogWarning("AssignSectorToHolderCommand: Holder not found");
                response.Message = "Holder not found";

                return response;
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

            response.Success = true;
            response.Sectors = sectors.Select(s => new SectorDto
            {
                Id = s.Id,
                Name = s.Name
            }).ToList();

            response.Holder = new HolderDto
            {
                Id = holder.Id,
                Name = holder.Name,
                Sectors = holder.Sectors
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "AssignSectorToHolderCommand: {@Request}", request);
            response.Message = ex.Message;
        }

        return response;
    }
}