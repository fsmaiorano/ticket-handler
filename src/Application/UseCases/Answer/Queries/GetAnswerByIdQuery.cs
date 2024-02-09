using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Answer.Queries;

public record GetAnswerByIdQuery : IRequest<GetAnswerByIdResponse>
{
    public required Guid Id { get; init; }
}

public class GetAnswerByIdResponse : BaseResponse
{
    public AnswerDto? Answer { get; set; }
}

public class GetAnswerByIdHandler(ILogger<GetAnswerByIdHandler> logger, IDataContext context) : IRequestHandler<GetAnswerByIdQuery, GetAnswerByIdResponse>
{
    private readonly IDataContext _context = context;
    private readonly ILogger<GetAnswerByIdHandler> _logger = logger;

    public async Task<GetAnswerByIdResponse> Handle(GetAnswerByIdQuery request, CancellationToken cancellationToken)
    {
        var response = new GetAnswerByIdResponse();

        try
        {
            _logger.LogInformation("GetAnswerById: {@Request}", request);

            var answer = await _context.Answers.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (answer is null)
            {
                _logger.LogWarning("GetAnswerById: Answer not");
                response.Message = "Answer not found";

                return response;
            }

            response.Success = true;
            response.Message = "Answer found";
            response.Answer = new AnswerDto
            {
                Content = answer.Content,
                TicketId = answer.TicketId,
                UserId = answer.UserId,
                HolderId = answer.HolderId,
                SectorId = answer.SectorId
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetAnswerById: {@Request}", request);
            response.Message = ex.Message;
        }

        return response;
    }
}
