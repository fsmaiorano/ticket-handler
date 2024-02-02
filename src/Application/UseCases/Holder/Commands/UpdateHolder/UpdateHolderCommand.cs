using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Holder.Commands.UpdateHolder;

public record UpdateHolderCommand : IRequest<Guid?>
{
    public Guid Id { get; set; }
    public string? Name { get; set; } = string.Empty;
}

public class UpdateHolderHandler(ILogger<UpdateHolderHandler> logger, IDataContext context) : IRequestHandler<UpdateHolderCommand, Guid?>
{
    private readonly IDataContext _context = context;
    private readonly ILogger<UpdateHolderHandler> _logger = logger;

    public async Task<Guid?> Handle(UpdateHolderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("UpdateHolderCommand: {@Request}", request);

            var holder = await _context.Holders.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (holder is null)
            {
                _logger.LogWarning("UpdateHolderCommand: Holder not found");
                return null;
            }

            if (!string.IsNullOrWhiteSpace(request.Name))
                holder.Name = request.Name;

            _context.Holders.Update(holder);
            await _context.SaveChangesAsync(cancellationToken);

            return holder.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "UpdateHolderCommand: {@Request}", request);
            throw;
        }
    }
}