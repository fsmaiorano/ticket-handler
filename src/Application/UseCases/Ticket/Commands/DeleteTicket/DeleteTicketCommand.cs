using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Ticket.Commands.DeleteTicket;

public record DeleteTicketCommand : IRequest<Guid?>
{
    public Guid Id { get; init; }
}

public class DeleteTicketHandler(ILogger<DeleteTicketHandler> logger, IDataContext context) : IRequestHandler<DeleteTicketCommand, Guid?>
{
    private readonly IDataContext _context = context;
    private readonly ILogger<DeleteTicketHandler> _logger = logger;

    public async Task<Guid?> Handle(DeleteTicketCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("DeleteTicketCommand: {@Request}", request);

            var ticket = await _context.Tickets.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (ticket is null)
            {
                _logger.LogWarning("DeleteTicketCommand: Ticket not found");
                return null;
            }

            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync(cancellationToken);

            return ticket.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "DeleteTicketCommand: {@Request}", request);
            throw;
        }
    }
}