﻿using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Ticket.Queries;

public record GetTicketByIdQuery : IRequest<TicketEntity?>
{
    public required Guid Id { get; init; }
}

public class GetTicketByIdHandler(ILogger<GetTicketByIdHandler> logger, IDataContext context) : IRequestHandler<GetTicketByIdQuery, TicketEntity?>
{
    private readonly IDataContext _context = context;
    private readonly ILogger<GetTicketByIdHandler> _logger = logger;

    public async Task<TicketEntity?> Handle(GetTicketByIdQuery request, CancellationToken cancellationToken)
    {
        TicketEntity? ticket = default;

        try
        {
            _logger.LogInformation("GetTicketById: {@Request}", request);

            ticket = await _context.Tickets.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetTicketById: {@Request}", request);
        }

        return ticket;
    }
}
