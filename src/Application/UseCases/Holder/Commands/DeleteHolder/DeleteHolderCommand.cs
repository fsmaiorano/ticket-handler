using Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Holder.Commands.DeleteHolder;

public record DeleteHolderCommand : IRequest<Guid?>
{
    public Guid Id { get; set; }
}

public class DeleteHolderHandler(ILogger<DeleteHolderHandler> logger, IDataContext context) : IRequestHandler<DeleteHolderCommand, Guid?>
{
    private readonly IDataContext _context = context;
    private readonly ILogger<DeleteHolderHandler> _logger = logger;

    public async Task<Guid?> Handle(DeleteHolderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("DeleteHolderCommand: {@Request}", request);

            var holder = await _context.Holders.FindAsync(request.Id, cancellationToken);

            if (holder is null)
            {
                return null;
            }

            _context.Holders.Remove(holder);
            await _context.SaveChangesAsync(cancellationToken);

            return holder.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "DeleteHolderCommand: {@Request}", request);
            throw;
        }
    }
}