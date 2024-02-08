using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Answer.Commands.CreateAnswer;

public record CreateAnswerCommand : IRequest<CreateAnswerResponse>
{
    public required string Content { get; set; }
    public required Guid TicketId { get; set; }
    public TicketEntity? Ticket { get; set; }
    public required Guid UserId { get; set; }
    public UserEntity? User { get; set; }
    public required Guid HolderId { get; set; }
    public required Guid SectorId { get; set; }
}

public class CreateAnswerResponse : BaseResponse
{
    public AnswerEntity? Answer { get; set; }
}

public class CreateAnswerHandler(ILogger<CreateAnswerHandler> logger, IDataContext context) : IRequestHandler<CreateAnswerCommand, CreateAnswerResponse>
{
    private readonly IDataContext _context = context;
    private readonly ILogger<CreateAnswerHandler> _logger = logger;

    public async Task<CreateAnswerResponse> Handle(CreateAnswerCommand request, CancellationToken cancellationToken)
    {
        var response = new CreateAnswerResponse();

        try
        {
            _logger.LogInformation("CreateAnswerCommand: {@Request}", request);

            var answer = new AnswerEntity
            {
                Content = request.Content,
                TicketId = request.TicketId,
                UserId = request.UserId,
                HolderId = request.HolderId,
                SectorId = request.SectorId
            };

            var asnwerExists = await _context.Answers.AnyAsync(x => x.TicketId == request.TicketId, cancellationToken);

            if (asnwerExists)
            {
                _logger.LogWarning("CreateAnswerCommand: Answer already exists");
                response.Message = "Answer already exists";

                return response;
            }

            await _context.Answers.AddAsync(answer, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            response.Success = true;
            response.Message = "Answer created";
            response.Answer = answer;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "CreateAnswerCommand: {@Request}", request);
            response.Message = ex.Message;
        }

        return response;
    }
}
