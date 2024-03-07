using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Ticket.Queries;

public record GetTicketByIdQuery : IRequest<GetTicketByIdResponse>
{
    public required Guid Id { get; init; }
}

public class GetTicketByIdResponse : BaseResponse
{
    public TicketDto? Ticket { get; set; }
}

public class GetTicketByIdHandler(ILogger<GetTicketByIdHandler> logger, IDataContext context) : IRequestHandler<GetTicketByIdQuery, GetTicketByIdResponse>
{
    private readonly IDataContext _context = context;
    private readonly ILogger<GetTicketByIdHandler> _logger = logger;

    public async Task<GetTicketByIdResponse> Handle(GetTicketByIdQuery request, CancellationToken cancellationToken)
    {
        var response = new GetTicketByIdResponse();

        try
        {
            _logger.LogInformation("GetTicketById: {@Request}", request);

            var ticket = await _context.Tickets.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (ticket is null)
            {
                _logger.LogWarning("GetTicketById: Ticket not");
                response.Message = "Ticket not found";

                return response;
            }

            var status = await _context.Statuses
                .Where(x => x.Id == ticket.StatusId)
                .Select(x => x.Code)
                .FirstOrDefaultAsync(cancellationToken);

            if (status is null)
            {
                _logger.LogWarning("GetTicketById: Status not found");
                response.Message = "Status not found";

                return response;
            }

            var priority = await _context.Priorities
                .Where(x => x.Id == ticket.PriorityId)
                .Select(x => x.Code)
                .FirstOrDefaultAsync(cancellationToken);

            if (priority is null)
            {
                _logger.LogWarning("GetTicketById: Priority not found");
                response.Message = "Priority not found";

                return response;
            }

            response.Success = true;
            response.Message = "Ticket found";
            response.Ticket = new TicketDto
            {
                Id = ticket.Id,
                Title = ticket.Title,
                Content = ticket.Content,
                Status = status,
                Priority = priority,
                UserId = ticket.UserId,
                HolderId = ticket.HolderId,
                SectorId = ticket.SectorId
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetTicketById: {@Request}", request);
            response.Message = ex.Message;
        }

        return response;
    }
}
