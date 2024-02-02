using Application.Common.Interfaces;
using Domain.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Ticket.Commands.UpdateTicket;

public record UpdateTicketCommand : IRequest<Guid?>
{
    public required Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public TicketStatus Status { get; set; }
    public TicketPriority Priority { get; set; }
    public required Guid HolderId { get; set; }
    public required Guid? SectorId { get; set; }
    public required Guid? AssigneeId { get; set; }
}

public class UpdateTicketCommandHandler(ILogger<UpdateTicketCommandHandler> logger, IDataContext context) : IRequestHandler<UpdateTicketCommand, Guid?>
{
    private readonly IDataContext _context = context;
    private readonly ILogger<UpdateTicketCommandHandler> _logger = logger;

    public async Task<Guid?> Handle(UpdateTicketCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("UpdateTicketCommand: {@Request}", request);

            var ticket = await _context.Tickets.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (ticket is null)
            {
                _logger.LogWarning("UpdateTicketCommand: Ticket not found");
                return null;
            }

            ticket.Title = request.Title;
            ticket.Content = request.Content;
            ticket.Status = request.Status;
            ticket.Priority = request.Priority;
            ticket.HolderId = request.HolderId;
            ticket.SectorId = request.SectorId;
            ticket.AssigneeId = request.AssigneeId;

            await _context.SaveChangesAsync(cancellationToken);

            return ticket.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "UpdateTicketCommand: {@Request}", request);
            return null;
        }
    }
}

