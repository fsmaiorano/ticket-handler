using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Holder.Commands.CreateHolder;

public record CreateHolderCommand : IRequest<Guid?>
{
    public required string Name { get; set; }
    public List<SectorEntity>? Sectors { get; set; }
}

public class CreateHolderHandler : IRequestHandler<CreateHolderCommand, Guid?>
{
    private readonly IDataContext _context;
    private readonly ILogger<CreateHolderHandler> _logger;

    public CreateHolderHandler(ILogger<CreateHolderHandler> logger, IDataContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<Guid?> Handle(CreateHolderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("CreateHolderCommand: {@Request}", request);

            var holder = new HolderEntity
            {
                Name = request.Name,
                Sectors = request.Sectors
            };

            _context.Holders.Add(holder);
            await _context.SaveChangesAsync(cancellationToken);

            var createdHolder = await _context.Holders.FindAsync([holder.Id], cancellationToken);

            return createdHolder?.Id ?? null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "CreateHolderCommand: {@Request}", request);
            throw;
        }
    }
}