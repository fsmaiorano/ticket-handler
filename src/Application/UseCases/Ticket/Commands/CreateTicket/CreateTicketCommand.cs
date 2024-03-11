using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Ticket.Commands.CreateTicket;

public record CreateTicketCommand : IRequest<CreateTicketResponse>
{
    public required string Title { get; set; }
    public required string Content { get; set; }
    public required string Status { get; set; }
    public required string Priority { get; set; }
    public required Guid UserId { get; set; }
    public required Guid HolderId { get; set; }
    public required Guid SectorId { get; set; }
    public Guid AssigneeId { get; set; }
}

public class CreateTicketResponse : BaseResponse
{
    public TicketDto? Ticket { get; set; }
}

public class CreateAnswerHandler(ILogger<CreateAnswerHandler> logger, IDataContext context) : IRequestHandler<CreateTicketCommand, CreateTicketResponse>
{
    private readonly IDataContext _context = context;
    private readonly ILogger<CreateAnswerHandler> _logger = logger;

    public async Task<CreateTicketResponse> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
    {
        var response = new CreateTicketResponse();

        try
        {
            _logger.LogInformation("CreateTicketCommand: {@Request}", request);

            var statusId = await _context.Statuses
                .Where(x => x.Code == request.Status)
                .Select(x => x.Id)  
                .FirstOrDefaultAsync(cancellationToken);

            var priorityId = await _context.Priorities
                .Where(x => x.Code == request.Priority)
                .Select(x => x.Id)
                .FirstOrDefaultAsync(cancellationToken);

            var ticket = new TicketEntity()
            {
                Title = request.Title,
                Content = request.Content,
                StatusId = statusId,
                PriorityId = priorityId,
                UserId = request.UserId,
                HolderId = request.HolderId,
                SectorId = request.SectorId,
            };

            if (request.AssigneeId != Guid.Empty)
                ticket.AssigneeId = request.AssigneeId;

            var ticketExists = await _context.Tickets.AnyAsync(x => x.Title == request.Title, cancellationToken);

            if (ticketExists)
            {
                _logger.LogWarning("CreateTicketCommand: Ticket already exists");
                response.Message = "Ticket already exists";

                return response;
            }

            await _context.Tickets.AddAsync(ticket, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            response.Success = true;
            response.Message = "Ticket created";
            response.Ticket = new TicketDto
            {
                Id = ticket.Id,
                Title = ticket.Title,
                Content = ticket.Content,
                Status = request.Status,
                Priority = request.Priority,
                UserId = ticket.UserId,
                HolderId = ticket.HolderId,
                SectorId = ticket.SectorId,
            };

            if (ticket.AssigneeId != Guid.Empty)
                response.Ticket.AssigneeId = ticket.AssigneeId;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "CreateTicketCommand: {@Request}", request);
            response.Message = ex.Message;
        }

        return response;
    }
}
