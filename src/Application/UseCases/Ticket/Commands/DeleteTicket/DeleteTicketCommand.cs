using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Ticket.Commands.DeleteTicket;

public record DeleteTicketCommand : IRequest<DeleteTicketResponse>
{
    public Guid Id { get; init; }
}

public class DeleteTicketResponse : BaseResponse
{

}

public class DeleteTicketHandler(ILogger<DeleteTicketHandler> logger, IDataContext context) : IRequestHandler<DeleteTicketCommand, DeleteTicketResponse>
{
    private readonly IDataContext _context = context;
    private readonly ILogger<DeleteTicketHandler> _logger = logger;

    public async Task<DeleteTicketResponse> Handle(DeleteTicketCommand request, CancellationToken cancellationToken)
    {
        var response = new DeleteTicketResponse();

        try
        {
            _logger.LogInformation("DeleteTicketCommand: {@Request}", request);

            var ticket = await _context.Tickets.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (ticket is null)
            {
                _logger.LogWarning("DeleteTicketCommand: Ticket not found");
                response.Message = "Ticket not found";
                
                return response;
            }

            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync(cancellationToken);

            response.Success = true;
            response.Message = "Ticket deleted successfully";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "DeleteTicketCommand: {@Request}", request);
            response.Message = ex.Message;
        }

        return response;
    }
}