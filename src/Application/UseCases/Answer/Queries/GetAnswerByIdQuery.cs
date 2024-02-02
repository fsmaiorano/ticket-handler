using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Answer.Queries;

public record GetAnswerByIdQuery : IRequest<AnswerEntity?>
{
    public required Guid Id { get; init; }
}

public class GetAnswerByIdHandler(ILogger<GetAnswerByIdHandler> logger, IDataContext context) : IRequestHandler<GetAnswerByIdQuery, AnswerEntity?>
{
    private readonly IDataContext _context = context;
    private readonly ILogger<GetAnswerByIdHandler> _logger = logger;

    public async Task<AnswerEntity?> Handle(GetAnswerByIdQuery request, CancellationToken cancellationToken)
    {
        AnswerEntity? ticket = default;

        try
        {
            _logger.LogInformation("GetAnswerById: {@Request}", request);

            ticket = await _context.Answers.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetAnswerById: {@Request}", request);
        }

        return ticket;
    }
}
