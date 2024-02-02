using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Answer.Commands.DeleteAnswer;

public record DeleteAnswerCommand : IRequest<Guid?>
{
    public Guid Id { get; init; }
}

public class DeleteAnswerHandler(ILogger<DeleteAnswerHandler> logger, IDataContext context) : IRequestHandler<DeleteAnswerCommand, Guid?>
{

    private readonly IDataContext _context = context;
    private readonly ILogger<DeleteAnswerHandler> _logger = logger;

    public async Task<Guid?> Handle(DeleteAnswerCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("DeleteAnswerCommand: {@Request}", request);

            var answer = await _context.Answers.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (answer is null)
            {
                _logger.LogWarning("DeleteAnswerCommand: Answer not found");
                return null;
            }

            _context.Answers.Remove(answer);
            await _context.SaveChangesAsync(cancellationToken);

            return answer.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "DeleteAnswerCommand: {@Request}", request);
            throw;
        }
    }
}