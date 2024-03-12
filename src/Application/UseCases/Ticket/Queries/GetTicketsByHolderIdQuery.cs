using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Ticket.Queries;

public record GetTicketsByHolderIdQuery : IRequest<GetTicketsByHolderIdResponse>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public Guid HolderId { get; set; }
    public string? Sector { get; set; }
    public string? Status { get; set; }
    public string? Title { get; set; }
    public string? Priority { get; set; }
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

            IQueryable<TicketEntity> tickets = _context.Tickets
                                               .Where(x => x.HolderId == request.HolderId)
                                               .Include(x => x.Status)
                                               .Include(x => x.Priority)
                                               .Include(x => x.Sector)
                                               .Include(x => x.Answers)
                                               .Include(x => x.User)
                                               .AsNoTracking();

            if (!string.IsNullOrEmpty(request.Title))
                tickets = tickets.Where(x => EF.Functions.Like(x.Title, $"%{request.Title}%"));

            if (!string.IsNullOrEmpty(request.Sector) && !request.Sector.Equals("all", StringComparison.CurrentCultureIgnoreCase))
                tickets = tickets.Where(x => EF.Functions.Like(x.Sector!.Name, request.Sector));

            if (!string.IsNullOrEmpty(request.Status) && !request.Status.Equals("all", StringComparison.CurrentCultureIgnoreCase))
                tickets = tickets.Where(x => EF.Functions.Like(x.Status!.Code, request.Status));

            if (!string.IsNullOrEmpty(request.Priority) && !request.Priority.Equals("all", StringComparison.CurrentCultureIgnoreCase))
                tickets = tickets.Where(x => EF.Functions.Like(x.Priority!.Code, request.Priority));

            var paginatedList = await PaginatedList<TicketEntity>.CreateAsync(tickets, request.PageNumber, request.PageSize);

            if (paginatedList is null)
            {
                _logger.LogWarning("GetTicketsByHolderId: Tickets not");
                response.Message = "Tickets not found";

                return response;
            }

            response.Success = true;
            response.Message = "Tickets found";
            response.PageNumber = paginatedList.PageNumber;
            response.TotalPages = paginatedList.TotalPages;
            response.Tickets = paginatedList.Items.Select(x => new TicketDto
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
                SectorId = x.SectorId,
                AssigneeId = x.AssigneeId,
            }).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetTicketsByHolderId: {@Request}", request);
            response.Success = false;
            response.Message = ex.Message;
        }

        return response;
    }
}