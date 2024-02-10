using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Constants;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Ticket.Commands.CreateTicket;

public record CreateTicketCommand : IRequest<CreateTicketResponse>
{
    public required string Title { get; set; }
    public required string Content { get; set; }
    public required TicketStatus Status { get; set; }
    public required TicketPriority Priority { get; set; }
    public required Guid UserId { get; set; }
    public required Guid HolderId { get; set; }
    public required Guid SectorId { get; set; }
    public required Guid AssigneeId { get; set; }
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

            var ticket = new TicketEntity
            {
                Title = request.Title,
                Content = request.Content,
                Status = request.Status,
                Priority = request.Priority,
                UserId = request.UserId,
                HolderId = request.HolderId,
                SectorId = request.SectorId,
                AssigneeId = request.AssigneeId
            };

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
                Status = ticket.Status,
                Priority = ticket.Priority,
                UserId = ticket.UserId,
                HolderId = ticket.HolderId,
                SectorId = ticket.SectorId,
                AssigneeId = ticket.AssigneeId
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "CreateTicketCommand: {@Request}", request);
            response.Message = ex.Message;
        }

        return response;
    }
}
