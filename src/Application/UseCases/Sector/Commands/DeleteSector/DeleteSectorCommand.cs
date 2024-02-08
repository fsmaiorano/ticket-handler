using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.User.Commands.SectorUser;

public record DeleteSectorCommand : IRequest<DeleteSectorResponse>
{
    public Guid Id { get; set; }
}

public class DeleteSectorResponse : BaseResponse
{

}

public class SectorUserHandler(ILogger<SectorUserHandler> logger, IDataContext context) : IRequestHandler<DeleteSectorCommand, DeleteSectorResponse>
{
    private readonly IDataContext _context = context;
    private readonly ILogger<SectorUserHandler> _logger = logger;

    public async Task<DeleteSectorResponse> Handle(DeleteSectorCommand request, CancellationToken cancellationToken)
    {
        var response = new DeleteSectorResponse();

        try
        {
            _logger.LogInformation("DeleteSectorCommand: {@Request}", request);

            var Sector = await _context.Sectors.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (Sector is null)
            {
                _logger.LogWarning("DeleteSectorCommand: Sector not found");
                response.Message = "Sector not found";

                return response;
            }

            _context.Sectors.Remove(Sector);
            await _context.SaveChangesAsync(cancellationToken);

            response.Success = true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "DeleteSectorCommand: {@Request}", request);
            response.Message = ex.Message;
        }

        return response;
    }
}
