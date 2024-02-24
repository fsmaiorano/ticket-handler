using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Ticket.Commands.UpdateTicket;

public record UpdateTicketCommand : IRequest<UpdateTicketResponse>
{
    public required Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public TicketStatus? Status { get; set; }
    public TicketPriority? Priority { get; set; }
    public required Guid HolderId { get; set; }
    public required Guid SectorId { get; set; }
    public Guid AssigneeId { get; set; }
}

public class UpdateTicketResponse : BaseResponse
{

}

public class UpdateTicketCommandHandler(ILogger<UpdateTicketCommandHandler> logger, IDataContext context) : IRequestHandler<UpdateTicketCommand, UpdateTicketResponse>
{
    private readonly IDataContext _context = context;
    private readonly ILogger<UpdateTicketCommandHandler> _logger = logger;

    public async Task<UpdateTicketResponse> Handle(UpdateTicketCommand request, CancellationToken cancellationToken)
    {
        var response = new UpdateTicketResponse();

        try
        {
            _logger.LogInformation("UpdateTicketCommand: {@Request}", request);

            var ticket = await _context.Tickets.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (ticket is null)
            {
                _logger.LogWarning("UpdateTicketCommand: Ticket not found");
                response.Message = "Ticket not found";

                return response;
            }

            if (request.Title != null)
                ticket.Title = request.Title;

            if (request.Content != null)
                ticket.Content = request.Content;

            if (request.Status != null)
                ticket.Status = request.Status.Value;

            if (request.Priority != null)
                ticket.Priority = request.Priority.Value;

            await _context.SaveChangesAsync(cancellationToken);

            response.Success = true;
            response.Message = "Ticket updated";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "UpdateTicketCommand: {@Request}", request);
            response.Message = "Error updating ticket";
        }

        return response;
    }
}

