using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Answer.Commands.UpdateAnswer;

public record UpdateAnswerCommand : IRequest<UpdateAnswerResponse>
{
    public required Guid Id { get; init; }
    public required string Content { get; init; }
    public required Guid TicketId { get; init; }
    public required Guid UserId { get; init; }
    public required Guid HolderId { get; init; }
    public required Guid SectorId { get; init; }
}

public class UpdateAnswerResponse : BaseResponse
{

}

public class UpdateAnswerHandler(ILogger<UpdateAnswerHandler> logger, IDataContext context) : IRequestHandler<UpdateAnswerCommand, UpdateAnswerResponse>
{
    private readonly IDataContext _context = context;
    private readonly ILogger<UpdateAnswerHandler> _logger = logger;

    public async Task<UpdateAnswerResponse> Handle(UpdateAnswerCommand request, CancellationToken cancellationToken)
    {
        var response = new UpdateAnswerResponse();

        try
        {
            _logger.LogInformation("UpdateAnswerCommand: {@Request}", request);

            var answer = await _context.Answers.FindAsync(new object[] { request.Id }, cancellationToken);

            if (answer is null)
            {
                _logger.LogWarning("UpdateAnswerCommand: Answer not found");
                response.Message = "Answer not found";

                return response;
            }

            if (request.Content != null)
            {
                answer.Content = request.Content;
            }

            await _context.SaveChangesAsync(cancellationToken);

            response.Success = true;
            response.Message = "Answer updated";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "UpdateAnswerCommand: {@Request}", request);
            response.Message = "Error updating answer";
        }

        return response;
    }
}
