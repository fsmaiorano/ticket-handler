﻿using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Holder.Queries;

public record GetHolderByIdQuery : IRequest<GetHolderByIdResponse>
{
    public required Guid Id { get; init; }
}

public class GetHolderByIdResponse : BaseResponse
{
    public HolderDto? Holder { get; set; }
}

public class GetHolderByIdHandler(ILogger<GetHolderByIdHandler> logger, IDataContext context) : IRequestHandler<GetHolderByIdQuery, GetHolderByIdResponse>
{
    private readonly IDataContext _context = context;
    private readonly ILogger<GetHolderByIdHandler> _logger = logger;

    public async Task<GetHolderByIdResponse> Handle(GetHolderByIdQuery request, CancellationToken cancellationToken)
    {
        var response = new GetHolderByIdResponse();

        try
        {
            _logger.LogInformation("GetHolderById: {@Request}", request);

            var holder = await _context.Holders
                .Include(h => h.Sectors)
                .FirstOrDefaultAsync(h => h.Id == request.Id, cancellationToken);

            if (holder is null)
            {
                _logger.LogWarning("GetHolderById: Holder not found");
                response.Message = "Holder not found";

                return response;
            }

            response.Success = true;
            response.Message = "Holder found";
            response.Holder = new HolderDto
            {
                Id = holder.Id,
                Name = holder.Name,
                Sectors = holder.Sectors?.Select(s => new SectorDto
                               {
                    Id = s.Id,
                    Name = s.Name
                }).ToList()
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetHolderById: {@Request}", request);
            response.Message = ex.Message;
        }

        return response;
    }
}
