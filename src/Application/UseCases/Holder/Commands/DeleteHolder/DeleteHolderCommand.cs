using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Holder.Commands.DeleteHolder;

public record DeleteHolderCommand : IRequest<DeleteHolderResponse>
{
    public Guid Id { get; set; }
}

public class DeleteHolderResponse : BaseResponse
{

}

public class DeleteHolderHandler(ILogger<DeleteHolderHandler> logger, IDataContext context) : IRequestHandler<DeleteHolderCommand, DeleteHolderResponse>
{
    private readonly IDataContext _context = context;
    private readonly ILogger<DeleteHolderHandler> _logger = logger;

    public async Task<DeleteHolderResponse> Handle(DeleteHolderCommand request, CancellationToken cancellationToken)
    {
        var response = new DeleteHolderResponse();

        try
        {
            _logger.LogInformation("DeleteHolderCommand: {@Request}", request);

            var holder = await _context.Holders.FindAsync(request.Id, cancellationToken);

            if (holder is null)
            {
                _logger.LogWarning("DeleteHolderCommand: Holder not found");
                response.Message = "Holder not found";

                return response;
            }

            _context.Holders.Remove(holder);
            await _context.SaveChangesAsync(cancellationToken);

            response.Success = true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "DeleteHolderCommand: {@Request}", request);
            response.Message = ex.Message;
        }

        return response;
    }
}