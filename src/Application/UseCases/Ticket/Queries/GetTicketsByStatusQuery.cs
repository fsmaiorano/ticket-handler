using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Ticket.Queries;

public record GetTicketsByStatusQuery : IRequest<GetTicketsByStatusResponse>
{
    public required string Status { get; set; }
}

public class GetTicketsByStatusResponse : BaseResponse
{
    public List<TicketDto>? Tickets { get; set; }
}

public class GetTicketByStatusHandler(ILogger<GetTicketByStatusHandler> logger, IDataContext context) : IRequestHandler<GetTicketsByStatusQuery, GetTicketsByStatusResponse>
{
    private readonly IDataContext _context = context;
    private readonly ILogger<GetTicketByStatusHandler> _logger = logger;

    public async Task<GetTicketsByStatusResponse> Handle(GetTicketsByStatusQuery request, CancellationToken cancellationToken)
    {
        var response = new GetTicketsByStatusResponse();

        try
        {
            _logger.LogInformation("GetTicketsByStatus: {@Request}", request);

            var tickets = await _context.Tickets.Where(x => x.Status.ToString() == request.Status).ToListAsync(cancellationToken);

            if (tickets is null)
            {
                _logger.LogWarning("GetTicketsByStatus: Tickets not");
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
            _logger.LogError(ex, "GetTicketsByStatus: {@Request}", request);
            response.Message = ex.Message;
        }

        return response;
    }
}