using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.User.Commands.HolderUser;

public record DeleteHolderCommand : IRequest<Guid?>
{
    public Guid Id { get; set; }
}

public class HolderUserHandler(ILogger<HolderUserHandler> logger, IDataContext context) : IRequestHandler<DeleteHolderCommand, Guid?>
{
    private readonly IDataContext _context = context;
    private readonly ILogger<HolderUserHandler> _logger = logger;

    public async Task<Guid?> Handle(DeleteHolderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("DeleteHolderCommand: {@Request}", request);

            var holder = await _context.Holders.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (holder is null)
            {
                _logger.LogWarning("DeleteHolderCommand: Holder not found");
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
