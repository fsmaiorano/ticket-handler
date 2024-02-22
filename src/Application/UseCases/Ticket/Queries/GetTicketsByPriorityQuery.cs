using Application.Common.Interfaces;
using Application.Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Ticket.Queries;

public record GetTicketsByPriorityQuery
{
    public required string Priority { get; set; }
}

public class GetTicketsByPriorityResponse : BaseResponse
{
    public List<TicketDto>? Tickets { get; set; }
}

public class GetTicketByPriorityHandler(ILogger<GetTicketByPriorityHandler> logger, IDataContext context)
{
    private readonly IDataContext _context = context;
    private readonly ILogger<GetTicketByPriorityHandler> _logger = logger;

    public async Task<GetTicketsByPriorityResponse> Handle(GetTicketsByPriorityQuery request, CancellationToken cancellationToken)
    {
        var response = new GetTicketsByPriorityResponse();

        try
        {
            _logger.LogInformation("GetTicketsByPriority: {@Request}", request);

            var tickets = await _context.Tickets.Where(x => x.Priority.ToString() == request.Priority).ToListAsync(cancellationToken);

            if (tickets is null)
            {
                _logger.LogWarning("GetTicketsByPriority: Tickets not");
                response.Message = "Tickets not found";

                return response;
            }

            response.Success = true;
            response.Message = "Tickets found";
            response.Tickets = tickets.Select(x => new TicketDto
            {
                Id = x.Id,
                Title = x.Title,
                Content = x.Content,
                UserId = x.UserId,
                HolderId = x.HolderId,
                SectorId = x.SectorId
            }).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetTicketsByPriority");
            response.Message = "Error on get tickets by priority";
        }

        return response;
    }
}