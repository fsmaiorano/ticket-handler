using Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Answer.Commands.UpdateAnswer;

public record UpdateAnswerCommand : IRequest<Guid?>
{
    public required Guid Id { get; init; }
    public required string Content { get; init; }
    public required Guid TicketId { get; init; }
    public required Guid UserId { get; init; }
    public required Guid HolderId { get; init; }
    public required Guid SectorId { get; init; }
}

public class UpdateAnswerHandler(ILogger<UpdateAnswerHandler> logger, IDataContext context) : IRequestHandler<UpdateAnswerCommand, Guid?>
{
    private readonly IDataContext _context = context;
    private readonly ILogger<UpdateAnswerHandler> _logger = logger;

    public async Task<Guid?> Handle(UpdateAnswerCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("UpdateAnswerCommand: {@Request}", request);

            var answer = await _context.Answers.FindAsync(new object[] { request.Id }, cancellationToken);

            if (answer is null)
            {
                _logger.LogWarning("UpdateAnswerCommand: Answer not found");
                return null;
                //throw new NotFoundException(nameof(AnswerEntity), request.Id);
            }

            if (request.Content != null)
                answer.Content = request.Content;

            await _context.SaveChangesAsync(cancellationToken);

            return answer.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "UpdateAnswerCommand: {@Request}", request);
            throw;
        }
    }
}
