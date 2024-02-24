using Application.Common.Interfaces;
using Application.Common.Mapping;
using Application.Common.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application;

public record GetAnswersByTicketIdQuery : IRequest<GetAnswersByTicketIdResponse>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public Guid TicketId { get; set; }
}

public class GetAnswersByTicketIdResponse : PaginatedBaseResponse
{

    public List<AnswerDto>? Answers { get; set; }
}

public class GetAnswersByTicketIdHandler(ILogger<GetAnswersByTicketIdHandler> logger, IDataContext context) : IRequestHandler<GetAnswersByTicketIdQuery, GetAnswersByTicketIdResponse>
{
    private readonly IDataContext _context = context;
    private readonly ILogger<GetAnswersByTicketIdHandler> _logger = logger;

    public async Task<GetAnswersByTicketIdResponse> Handle(GetAnswersByTicketIdQuery request, CancellationToken cancellationToken)
    {
        var response = new GetAnswersByTicketIdResponse();

        try
        {
            _logger.LogInformation("GetAnswersByTicketId: {@Request}", request);

            var answers = await _context.Answers.Where(x => x.TicketId == request.TicketId)
                                                .PaginatedListAsync(request.PageNumber, request.PageSize);

            if (answers is null)
            {
                _logger.LogWarning("GetAnswersByTicketId: Answers not");
                response.Message = "Answers not found";

                return response;
            }

            response.Success = true;
            response.Message = "Answer found";
            response.PageNumber = answers.PageNumber;
            response.TotalPages = answers.TotalPages;
            response.Answers = answers.Items.Select(x => new AnswerDto
                       {
                Id = x.Id,
                Content = x.Content,
                TicketId = x.TicketId,
                UserId = x.UserId,
                HolderId = x.HolderId,
                SectorId = x.SectorId
            }).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError("GetAnswersByTicketId: {@Exception}", ex);
            response.Message = "Error getting answer";
        }

        return response;
    }
}