using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.User.Commands.AssignUserToSector;
public record AssignUserToSectorCommand : IRequest<AssignUserToSectorResponse>
{
    public required Guid UserId { get; set; }
    public required List<Guid> SectorsId { get; set; }
}

public class AssignUserToSectorResponse : BaseResponse
{

}

public class AssignUserToSectorHandler(ILogger<AssignUserToSectorHandler> logger, IDataContext context) : IRequestHandler<AssignUserToSectorCommand, AssignUserToSectorResponse>
{
    private readonly IDataContext _context = context;
    private readonly ILogger<AssignUserToSectorHandler> _logger = logger;

    public async Task<AssignUserToSectorResponse> Handle(AssignUserToSectorCommand request, CancellationToken cancellationToken)
    {
        var response = new AssignUserToSectorResponse();

        try
        {
            _logger.LogInformation("AssignUserToSectorCommand: {@Request}", request);

            var user = await _context.Users.FindAsync([request.UserId], cancellationToken);

            if (user is null)
            {
                _logger.LogWarning("AssignUserToSectorCommand: User not found");
                response.Message = "User not found";

                return response;
            }

            var sectors = await _context.Sectors
                                .Where(s => request.SectorsId.Contains(s.Id))
                                .ToListAsync(cancellationToken);

            if (sectors.Count > 0)
            {
                var sectorsNotAssigned = sectors.Where(s => user.Sectors is not null &&
                                                            !user.Sectors
                                                                .Any(us => us.Id == s.Id))
                                                                .ToList();

                if (sectorsNotAssigned.Count > 0 && user.Sectors != null)
                {
                    user.Sectors.AddRange(sectorsNotAssigned);
                    await _context.SaveChangesAsync(cancellationToken);
                }
            }

            response.Success = true;
            response.Message = "User assigned to sector";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "AssignUserToSectorCommand: {@Request}", request);
            response.Message = ex.Message;
        }

        return response;
    }
}