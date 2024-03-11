using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Holder.Commands.CreateHolder;

public record CreateHolderCommand : IRequest<CreateHolderResponse>
{
    public required string Name { get; set; }
    public List<SectorEntity>? Sectors { get; set; }
}

public class CreateHolderResponse : BaseResponse
{
    public HolderDto? Holder { get; set; }
}

public class CreateHolderHandler(ILogger<CreateHolderHandler> logger, IDataContext context) : IRequestHandler<CreateHolderCommand, CreateHolderResponse>
{
    private readonly IDataContext _context = context;
    private readonly ILogger<CreateHolderHandler> _logger = logger;

    public async Task<CreateHolderResponse> Handle(CreateHolderCommand request, CancellationToken cancellationToken)
    {
        var response = new CreateHolderResponse();

        try
        {
            _logger.LogInformation("CreateHolderCommand: {@Request}", request);

            var holder = new HolderEntity
            {
                Name = request.Name,
                Sectors = request.Sectors
            };

            var holderExists = await _context.Holders.AnyAsync(x => x.Name == request.Name, cancellationToken);

            if (holderExists)
            {
                _logger.LogWarning("CreateHolderCommand: Holder already exists");
                response.Message = "Holder already exists";

                return response;
            }

            _context.Holders.Add(holder);
            await _context.SaveChangesAsync(cancellationToken);

            var createdHolder = await _context.Holders.FindAsync([holder.Id], cancellationToken);

            if (createdHolder is null)
            {
                _logger.LogWarning("CreateHolderCommand: Error creating holder");
                response.Message = "Error creating holder";
                return response;
            }

            response.Success = true;
            response.Holder = new HolderDto
            {
                Id = createdHolder.Id,
                Name = createdHolder.Name,
                Sectors = createdHolder.Sectors?.Select(s => new SectorDto
                               {
                    Id = s.Id,
                    Name = s.Name
                }).ToList()
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "CreateHolderCommand: {@Request}", request);
            response.Message = ex.Message;
        }

        return response;
    }
}