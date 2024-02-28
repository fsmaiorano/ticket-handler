using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.User.Commands.AssignUserToTicket;

public record AssignUserToTicketCommand : IRequest<AssignUserToTicketResponse>
{
    public Guid UserId { get; set; }
    public Guid TicketId { get; set; }
}

public class AssignUserToTicketResponse : BaseResponse
{

}

public class AssignUserToTicketHandler(ILogger<AssignUserToTicketHandler> logger, IDataContext context) : IRequestHandler<AssignUserToTicketCommand, AssignUserToTicketResponse>
{
    private readonly IDataContext _context = context;
    private readonly ILogger<AssignUserToTicketHandler> _logger = logger;

    public async Task<AssignUserToTicketResponse> Handle(AssignUserToTicketCommand request, CancellationToken cancellationToken)
    {
        var response = new AssignUserToTicketResponse();

        try
        {
            _logger.LogInformation("AssignUserToTicketCommand: {@Request}", request);

            var user = await _context.Users.FindAsync([request.UserId], cancellationToken);

            if (user is null)
            {
                _logger.LogWarning("AssignUserToTicketCommand: User not found");
                response.Message = "User not found";

                return response;
            }

            var ticket = await _context.Tickets.FindAsync([request.TicketId], cancellationToken);

            if (ticket is null)
            {
                _logger.LogWarning("AssignUserToTicketCommand: Ticket not found");
                response.Message = "Ticket not found";

                return response;
            }

            ticket.AssigneeId = user.Id;
            _context.Tickets.Update(ticket);
            await _context.SaveChangesAsync(cancellationToken);

            response.Success = true;
            response.Message = "User assigned to ticket";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "AssignUserToTicketCommand: Error");
            response.Message = "Error";
        }

        return response;
    }
}