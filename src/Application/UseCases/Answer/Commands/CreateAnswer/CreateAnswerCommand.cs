using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Answer.Commands.CreateAnswer;

public record CreateAnswerCommand : IRequest<Guid?>
{
    public required string Content { get; set; }
    public required Guid TicketId { get; set; }
    public TicketEntity? Ticket { get; set; }
    public required Guid UserId { get; set; }
    public UserEntity? User { get; set; }
    public required Guid HolderId { get; set; }
    public required Guid SectorId { get; set; }
}

public class CreateAnswerHandler(ILogger<CreateAnswerHandler> logger, IDataContext context) : IRequestHandler<CreateAnswerCommand, Guid?>
{
    private readonly IDataContext _context = context;
    private readonly ILogger<CreateAnswerHandler> _logger = logger;

    public async Task<Guid?> Handle(CreateAnswerCommand request, CancellationToken cancellationToken)
    {
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

            await _context.Answers.AddAsync(answer, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return answer.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "CreateAnswerCommand: {@Request}", request);
            throw;
        }
    }
}
