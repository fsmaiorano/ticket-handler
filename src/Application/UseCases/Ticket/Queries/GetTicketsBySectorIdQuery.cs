using Application.Common.Interfaces;
using Application.Common.Mapping;
using Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Ticket.Queries;

public record GetTicketsBySectorIdQuery : IRequest<GetTicketsBySectorIdResponse>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public required Guid HolderId { get; set; }
    public required Guid SectorId { get; set; }
}

public class GetTicketsBySectorIdResponse : PaginatedBaseResponse
{
    public List<TicketDto>? Tickets { get; set; }
}

public class GetTicketBySectorIdHandler(ILogger<GetTicketByIdHandler> logger, IDataContext context) : IRequestHandler<GetTicketsBySectorIdQuery, GetTicketsBySectorIdResponse>
{
    private readonly IDataContext _context = context;
    private readonly ILogger<GetTicketByIdHandler> _logger = logger;

    public async Task<GetTicketsBySectorIdResponse> Handle(GetTicketsBySectorIdQuery request, CancellationToken cancellationToken)
    {
        var response = new GetTicketsBySectorIdResponse();

        try
        {
            _logger.LogInformation("GetTicketsBySectorId: {@Request}", request);

            var tickets = await _context.Tickets.Where(x => x.SectorId == request.SectorId && x.HolderId == request.HolderId)
                                                .Include(x => x.Status)
                                                .Include(x => x.Priority)
                                                .Include(x => x.Answers)
                                                .PaginatedListAsync(request.PageNumber, request.PageSize);

            if (tickets is null)
            {
                _logger.LogWarning("GetTicketsBySectorId: Tickets not");
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
                Status = x.Status!.Code,
                Priority = x.Priority!.Code,
                Answers = x.Answers?.Select(a => new AnswerDto
                {
                    Id = a.Id,
                    Content = a.Content,
                    UserId = a.UserId,
                    HolderId = a.HolderId,
                    SectorId = a.SectorId,
                    TicketId = a.TicketId
                }).ToList(),
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