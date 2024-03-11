using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Sector.Commands.UpdateSector;

public record UpdateSectorCommand : IRequest<UpdateSectorResponse>
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public Guid HolderId { get; set; }
}

public class UpdateSectorResponse : BaseResponse
{

}

public class UpdateSectorHandler(ILogger<UpdateSectorHandler> logger, IDataContext context) : IRequestHandler<UpdateSectorCommand, UpdateSectorResponse>
{
    private readonly IDataContext _context = context;
    private readonly ILogger<UpdateSectorHandler> _logger = logger;

    public async Task<UpdateSectorResponse> Handle(UpdateSectorCommand request, CancellationToken cancellationToken)
    {
        var response = new UpdateSectorResponse();

        try
        {
            _logger.LogInformation("UpdateSectorCommand: {@Request}", request);

            var Sector = await _context.Sectors.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (Sector is null)
            {
                _logger.LogWarning("UpdateSectorCommand: Sector not found");
                response.Message = "Sector not found";

                return response;
            }

            if (!string.IsNullOrWhiteSpace(request.Name))
                Sector.Name = request.Name;

            _context.Sectors.Update(Sector);
            await _context.SaveChangesAsync(cancellationToken);

            response.Success = true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "UpdateSectorCommand: {@Request}", request);
            response.Message = ex.Message;
        }

        return response;
    }
}
