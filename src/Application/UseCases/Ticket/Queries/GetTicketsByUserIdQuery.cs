using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Ticket.Queries;

public record GetTicketsByUserIdQuery : IRequest<GetTicketsByUserIdResponse>
{
    public required Guid UserId { get; set; }
}

public class GetTicketsByUserIdResponse : BaseResponse
{
    public List<TicketDto>? Tickets { get; set; }
}

public class GetTicketByUserIdHandler(ILogger<GetTicketByIdHandler> logger, IDataContext context) : IRequestHandler<GetTicketsByUserIdQuery, GetTicketsByUserIdResponse>
{
    private readonly IDataContext _context = context;
    private readonly ILogger<GetTicketByIdHandler> _logger = logger;

    public async Task<GetTicketsByUserIdResponse> Handle(GetTicketsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var response = new GetTicketsByUserIdResponse();

        try
        {
            _logger.LogInformation("GetTicketsByUserId: {@Request}", request);

            var tickets = await _context.Tickets.Where(x => x.UserId == request.UserId).ToListAsync(cancellationToken);

            if (tickets is null)
            {
                _logger.LogWarning("GetTicketsByUserId: Tickets not");
                response.Message = "Tickets not found";

                return response;
            }

            response.Success = true;
            response.Message = "Tickets found";
            response.Tickets = tickets.Select(x => new TicketDto
            {
                Id = x.Id,
                Title = x.Title,
                Content = x.Content,
                UserId = x.UserId,
                HolderId = x.HolderId,
                SectorId = x.SectorId
            }).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetTicketsByUserId: {@Request}", request);
            response.Message = ex.Message;
        }

        return response;
    }
}
