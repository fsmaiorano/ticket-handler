﻿using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Ticket.Queries;

public record GetTicketByIdQuery : IRequest<GetTicketByIdResponse>
{
    public required Guid Id { get; init; }
}

public class GetTicketByIdResponse : BaseResponse
{
    public TicketEntity? Ticket { get; set; }
}

public class GetTicketByIdHandler(ILogger<GetTicketByIdHandler> logger, IDataContext context) : IRequestHandler<GetTicketByIdQuery, GetTicketByIdResponse>
{
    private readonly IDataContext _context = context;
    private readonly ILogger<GetTicketByIdHandler> _logger = logger;

    public async Task<GetTicketByIdResponse> Handle(GetTicketByIdQuery request, CancellationToken cancellationToken)
    {
        var response = new GetTicketByIdResponse();

        try
        {
            _logger.LogInformation("GetTicketById: {@Request}", request);

            var ticket = await _context.Tickets.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (ticket is null)
            {
                _logger.LogWarning("GetTicketById: Ticket not");
                response.Message = "Ticket not found";
            }

            response.Success = true;
            response.Ticket = ticket;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetTicketById: {@Request}", request);
            response.Message = ex.Message;
        }

        return response;
    }
}
