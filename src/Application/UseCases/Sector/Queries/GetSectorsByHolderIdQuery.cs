using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Sector.Queries;

public record GetSectorsByHolderIdQuery : IRequest<GetSectorsByHolderIdResponse>
{
    public required Guid HolderId { get; init; }
}

public class GetSectorsByHolderIdResponse : BaseResponse
{
    public List<SectorDto>? Sectors { get; set; }
}

public class GetSectorsByHolderIdHandler(ILogger<GetSectorsByHolderIdHandler> logger, IDataContext context) : IRequestHandler<GetSectorsByHolderIdQuery, GetSectorsByHolderIdResponse>
{
    private readonly IDataContext _context = context;
    private readonly ILogger<GetSectorsByHolderIdHandler> _logger = logger;

    public async Task<GetSectorsByHolderIdResponse> Handle(GetSectorsByHolderIdQuery request, CancellationToken cancellationToken)
    {
        var response = new GetSectorsByHolderIdResponse();

        try
        {
            _logger.LogInformation("GetSectorsByHolderIdQuery.Handle");

            var sectors = await _context.Sectors.Where(x => x.HolderId == request.HolderId).ToListAsync(cancellationToken);

            if (sectors is null)
            {
                _logger.LogWarning("GetSectorsByHolderIdQuery.Handle: Sectors not found");
                response.Message = "Sectors not found";

                return response;
            }

            response.Success = true;
            response.Message = "Sectors found";
            response.Sectors = sectors.Select(s => new SectorDto
            {
                Id = s.Id,
                Name = s.Name
            }).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetSectorsByHolderIdQuery.Handle");
            response.Message = ex.Message;
        }

        return response;
    }
}

