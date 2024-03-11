using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Answer.Commands.DeleteAnswer;

public record DeleteAnswerCommand : IRequest<DeleteAnswerResponse>
{
    public Guid Id { get; init; }
}

public class DeleteAnswerResponse : BaseResponse
{

}

public class DeleteAnswerHandler(ILogger<DeleteAnswerHandler> logger, IDataContext context) : IRequestHandler<DeleteAnswerCommand, DeleteAnswerResponse>
{

    private readonly IDataContext _context = context;
    private readonly ILogger<DeleteAnswerHandler> _logger = logger;

    public async Task<DeleteAnswerResponse> Handle(DeleteAnswerCommand request, CancellationToken cancellationToken)
    {
        var response = new DeleteAnswerResponse();

        try
        {
            _logger.LogInformation("DeleteAnswerCommand: {@Request}", request);

            var answer = await _context.Answers.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (answer is null)
            {
                _logger.LogWarning("DeleteAnswerCommand: Answer not found");
                response.Message = "Answer not found";

                return response;
            }

            _context.Answers.Remove(answer);
            await _context.SaveChangesAsync(cancellationToken);

            response.Success = true;
            response.Message = "Answer deleted";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "DeleteAnswerCommand: {@Request}", request);
            response.Message = ex.Message;
        }

        return response;
    }
}