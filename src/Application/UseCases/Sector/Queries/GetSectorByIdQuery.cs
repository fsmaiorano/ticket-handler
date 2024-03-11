using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Sector.Queries;

public record GetSectorByIdQuery : IRequest<GetSectorByIdResponse>
{
    public required Guid Id { get; init; }
}

public class GetSectorByIdResponse : BaseResponse
{
    public SectorDto? Sector { get; set; }
}

public class GetSectorByIdHandler(ILogger<GetSectorByIdHandler> logger, IDataContext context) : IRequestHandler<GetSectorByIdQuery, GetSectorByIdResponse>
{
    private readonly IDataContext _context = context;
    private readonly ILogger<GetSectorByIdHandler> _logger = logger;

    public async Task<GetSectorByIdResponse> Handle(GetSectorByIdQuery request, CancellationToken cancellationToken)
    {
        var response = new GetSectorByIdResponse();

        try
        {
            _logger.LogInformation("GetSectorById: {@Request}", request);

            var sector = await _context.Sectors.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (sector is null)
            {
                _logger.LogWarning("GetSectorById: Sector not");
                response.Message = "Sector not found";

                return response;
            }

            response.Success = true;
            response.Message = "Sector found";
            response.Sector = new SectorDto
            {
                Id = sector.Id,
                Name = sector.Name
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetSectorById: {@Request}", request);
            response.Message = ex.Message;
        }

        return response;
    }
}
