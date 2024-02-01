﻿using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Holder.Queries;

public record GetHolderByIdQuery : IRequest<HolderEntity?>
{
    public required Guid Id { get; init; }
}

public class GetHolderByIdHandler : IRequestHandler<GetHolderByIdQuery, HolderEntity?>
{
    private readonly IDataContext _context;
    private readonly ILogger<GetHolderByIdHandler> _logger;

    public GetHolderByIdHandler(ILogger<GetHolderByIdHandler> logger, IDataContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<HolderEntity?> Handle(GetHolderByIdQuery request, CancellationToken cancellationToken)
    {
        HolderEntity? holder = default;

        try
        {
            _logger.LogInformation("GetHolderById: {@Request}", request);

            holder = await _context.Holders.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetHolderById: {@Request}", request);
        }

        return holder;
    }
}