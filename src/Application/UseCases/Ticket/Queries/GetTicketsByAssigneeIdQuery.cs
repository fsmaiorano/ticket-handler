using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Ticket.Queries;

public record GetTicketsByAssigneeIdQuery : IRequest<GetTicketsByAssigneeIdResponse>
{
    public Guid AssigneeId { get; set; }
}

public class GetTicketsByAssigneeIdResponse : BaseResponse
{
    public List<TicketDto>? Tickets { get; set; }
}

public class GetTicketsByAssigneeIdHandler : IRequestHandler<GetTicketsByAssigneeIdQuery, GetTicketsByAssigneeIdResponse>
{
    private readonly IDataContext _context;
    private readonly ILogger<GetTicketsByAssigneeIdHandler> _logger;

    public GetTicketsByAssigneeIdHandler(ILogger<GetTicketsByAssigneeIdHandler> logger, IDataContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<GetTicketsByAssigneeIdResponse> Handle(GetTicketsByAssigneeIdQuery request, CancellationToken cancellationToken)
    {
        var response = new GetTicketsByAssigneeIdResponse();

        try
        {
            _logger.LogInformation("GetTicketsByAssigneeId: {@Request}", request);

            var tickets = await _context.Tickets.Where(x => x.AssigneeId == request.AssigneeId).ToListAsync(cancellationToken);

            if (tickets is null)
            {
                _logger.LogWarning("GetTicketsByAssigneeId: Tickets not");
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
            _logger.LogError(ex, "GetTicketsByAssigneeId: {@Request}", request);
            response.Message = ex.Message;
        }

        return response;
    }
}