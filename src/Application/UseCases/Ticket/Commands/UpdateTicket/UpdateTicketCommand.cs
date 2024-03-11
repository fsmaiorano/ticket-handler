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
    public string? Status { get; set; }
    public string? Priority { get; set; }
    public required Guid HolderId { get; set; }
    public required Guid SectorId { get; set; }
    public Guid? AssigneeId { get; set; }
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

            if (request.HolderId != Guid.Empty)
            {
                var holder = await _context.Holders
                    .Where(x => x.Id == request.HolderId)
                    .FirstOrDefaultAsync(cancellationToken);

                if (holder is null)
                {
                    _logger.LogWarning("UpdateTicketCommand: Holder not found");
                    response.Message = "Holder not found";

                    return response;
                }

                if (holder.Id != ticket.HolderId)
                    ticket.HolderId = holder.Id;
            }

            if (request.SectorId != Guid.Empty)
            {
                var sector = await _context.Sectors
                    .Where(x => x.Id == request.SectorId)
                    .FirstOrDefaultAsync(cancellationToken);

                if (sector is null)
                {
                    _logger.LogWarning("UpdateTicketCommand: Sector not found");
                    response.Message = "Sector not found";

                    return response;
                }

                if (sector.Id != ticket.SectorId)
                    ticket.SectorId = sector.Id;
            }

            if (request.AssigneeId is not null && request.AssigneeId != Guid.Empty)
            {
                var assignee = await _context.Users
                    .Where(x => x.Id == request.AssigneeId)
                    .FirstOrDefaultAsync(cancellationToken);

                if (assignee is null)
                {
                    _logger.LogWarning("UpdateTicketCommand: Assignee not found");
                    response.Message = "Assignee not found";

                    return response;
                }

                if (assignee.Id != ticket.AssigneeId)
                    ticket.AssigneeId = assignee.Id;
            }

            if (request.Status != null)
            {
                var status = await _context.Statuses
                    .Where(x => x.Code == request.Status)
                    .FirstOrDefaultAsync(cancellationToken);

                if (status is null)
                {
                    _logger.LogWarning("UpdateTicketCommand: Status not found");
                    response.Message = "Status not found";

                    return response;
                }

                ticket.StatusId = status.Id;
            }

            if (request.Priority != null)
            {
                var priority = await _context.Priorities
                    .Where(x => x.Code == request.Priority)
                    .FirstOrDefaultAsync(cancellationToken);

                if (priority is null)
                {
                    _logger.LogWarning("UpdateTicketCommand: Priority not found");
                    response.Message = "Priority not found";

                    return response;
                }

                ticket.PriorityId = priority.Id;
            }

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

