using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Ticket.Queries;

public record GetTicketsBySectorIdQuery : IRequest<GetTicketsBySectorIdResponse>
{
    public Guid SectorId { get; set; }
}

public class GetTicketsBySectorIdResponse : BaseResponse
{
    public List<TicketDto>? Tickets { get; set; }
}

public class GetTicketBySectorHandler(ILogger<GetTicketByIdHandler> logger, IDataContext context) : IRequestHandler<GetTicketsBySectorIdQuery, GetTicketsBySectorIdResponse>
{
    private readonly IDataContext _context = context;
    private readonly ILogger<GetTicketByIdHandler> _logger = logger;

    public async Task<GetTicketsBySectorIdResponse> Handle(GetTicketsBySectorIdQuery request, CancellationToken cancellationToken)
    {
        var response = new GetTicketsBySectorIdResponse();

        try
        {
            _logger.LogInformation("GetTicketsBySectorId: {@Request}", request);

            var tickets = await _context.Tickets.Where(x => x.SectorId == request.SectorId).ToListAsync(cancellationToken);

            if (tickets is null)
            {
                _logger.LogWarning("GetTicketsBySectorId: Tickets not");
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
            _logger.LogError(ex, "GetTicketsBySectorId: {@Request}", request);
            response.Message = ex.Message;
        }

        return response;
    }
}