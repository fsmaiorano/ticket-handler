using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Holder.Commands.UpdateHolder;

public record UpdateHolderCommand : IRequest<UpdateHolderResponse>
{
    public Guid Id { get; set; }
    public string? Name { get; set; } = string.Empty;
}

public class UpdateHolderResponse : BaseResponse
{

}

public class UpdateHolderHandler(ILogger<UpdateHolderHandler> logger, IDataContext context) : IRequestHandler<UpdateHolderCommand, UpdateHolderResponse>
{
    private readonly IDataContext _context = context;
    private readonly ILogger<UpdateHolderHandler> _logger = logger;

    public async Task<UpdateHolderResponse> Handle(UpdateHolderCommand request, CancellationToken cancellationToken)
    {
        var response = new UpdateHolderResponse();

        try
        {
            _logger.LogInformation("UpdateHolderCommand: {@Request}", request);

            var holder = await _context.Holders.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (holder is null)
            {
                _logger.LogWarning("UpdateHolderCommand: Holder not found");
                response.Message = "Holder not found";

                return response;
            }

            if (!string.IsNullOrWhiteSpace(request.Name))
                holder.Name = request.Name;

            _context.Holders.Update(holder);
            await _context.SaveChangesAsync(cancellationToken);

            response.Success = true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "UpdateHolderCommand: {@Request}", request);
            response.Message = ex.Message;
        }

        return response;
    }
}