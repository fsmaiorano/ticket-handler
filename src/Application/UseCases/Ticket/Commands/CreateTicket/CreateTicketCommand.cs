using Application.Common.Interfaces;
using Domain.Constants;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Ticket.Commands.CreateTicket;

public record CreateTicketCommand : IRequest<Guid?>
{
    public required string Title { get; set; }
    public required string Content { get; set; }
    public required TicketStatus Status { get; set; }
    public required TicketPriority Priority { get; set; }
    public required Guid HolderId { get; set; }
    public required Guid? SectorId { get; set; }
    public required Guid? AssigneeId { get; set; }
}

public class CreateAnswerHandler(ILogger<CreateAnswerHandler> logger, IDataContext context) : IRequestHandler<CreateTicketCommand, Guid?>
{
    private readonly IDataContext _context = context;
    private readonly ILogger<CreateAnswerHandler> _logger = logger;

    public async Task<Guid?> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("CreateTicketCommand: {@Request}", request);

            var ticket = new TicketEntity
            {
                Title = request.Title,
                Content = request.Content,
                Status = request.Status,
                Priority = request.Priority,
                HolderId = request.HolderId,
                SectorId = request.SectorId,
                AssigneeId = request.AssigneeId
            };

            await _context.Tickets.AddAsync(ticket, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return ticket.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "CreateTicketCommand: {@Request}", request);
            throw;
        }
    }
}
