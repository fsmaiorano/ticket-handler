using Application.Common.Interfaces;
using Application.Common.Mapping;
using Application.Common.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Ticket.Queries;

public record GetTicketsByHolderIdQuery : IRequest<GetTicketsByHolderIdResponse>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public Guid HolderId { get; set; }
}

public class GetTicketsByHolderIdResponse : PaginatedBaseResponse
{

    public List<TicketDto>? Tickets { get; set; }
}

public class GetTicketByHolderHandler(ILogger<GetTicketByIdHandler> logger, IDataContext context) : IRequestHandler<GetTicketsByHolderIdQuery, GetTicketsByHolderIdResponse>
{
    private readonly IDataContext _context = context;
    private readonly ILogger<GetTicketByIdHandler> _logger = logger;

    public async Task<GetTicketsByHolderIdResponse> Handle(GetTicketsByHolderIdQuery request, CancellationToken cancellationToken)
    {
        var response = new GetTicketsByHolderIdResponse();

        try
        {
            _logger.LogInformation("GetTicketsByHolderId: {@Request}", request);

            var tickets = await _context.Tickets.Where(x => x.HolderId == request.HolderId)
                                                .PaginatedListAsync(request.PageNumber, request.PageSize);

            if (tickets is null)
            {
                _logger.LogWarning("GetTicketsByHolderId: Tickets not");
                response.Message = "Tickets not found";

                return response;
            }

            response.Success = true;
            response.Message = "Tickets found";
            response.PageNumber = tickets.PageNumber;
            response.TotalPages = tickets.TotalPages;
            response.Tickets = tickets.Items.Select(x => new TicketDto
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
            _logger.LogError(ex, "GetTicketsByHolderId: {@Request}", request);
            response.Message = ex.Message;
        }

        return response;
    }
}