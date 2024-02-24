using Application.Common.Interfaces;
using Application.Common.Mapping;
using Application.Common.Models;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Ticket.Queries;

public record GetTicketsByPriorityQuery
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public required string Priority { get; set; }
}

public class GetTicketsByPriorityResponse : PaginatedBaseResponse
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

            var tickets = await _context.Tickets.Where(x => x.Priority.ToString() == request.Priority)
                                                .PaginatedListAsync(request.PageNumber, request.PageSize);

            if (tickets is null)
            {
                _logger.LogWarning("GetTicketsByPriority: Tickets not");
                response.Message = "Tickets not found";

                return response;
            }

            response.Success = true;
            response.Message = "Tickets found";
            response.PageNumber = tickets.PageNumber;
            response.TotalPages = tickets.TotalPages;
            response.Tickets = tickets.Items.Select(x => new TicketDto
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