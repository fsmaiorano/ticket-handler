using Application.Common.Models;
using Application.UseCases.Ticket.Commands.CreateTicket;
using Application.UseCases.Ticket.Commands.DeleteTicket;
using Application.UseCases.Ticket.Commands.UpdateTicket;
using Application.UseCases.Ticket.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class TicketController : BaseController
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CreateTicketResponse>> Post(CreateTicketCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<TicketDto>> Get(Guid id)
        {
            var getTicketByIdResponse = await Mediator.Send(new GetTicketByIdQuery { Id = id });

            if (getTicketByIdResponse.Ticket is null)
                return NotFound();

            if (!getTicketByIdResponse.Success)
                return BadRequest(getTicketByIdResponse.Message);

            var ticketDto = new TicketDto
            {
                Id = getTicketByIdResponse.Ticket.Id,
                Title = getTicketByIdResponse.Ticket.Title,
                Content = getTicketByIdResponse.Ticket.Content,
                Status = getTicketByIdResponse.Ticket.Status,
                Priority = getTicketByIdResponse.Ticket.Priority,
                HolderId = getTicketByIdResponse.Ticket.HolderId,
                SectorId = getTicketByIdResponse.Ticket.SectorId,
                AssigneeId = getTicketByIdResponse.Ticket.AssigneeId,
                UserId = getTicketByIdResponse.Ticket.UserId,
                CreatedAt = getTicketByIdResponse.Ticket.CreatedAt,
                UpdatedAt = getTicketByIdResponse.Ticket.UpdatedAt,
            };

            return Ok(ticketDto);
        }

        [HttpGet("holder/{holderId}/sector/{sectorId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SectorDto>>> GetTicketsBySectorId(Guid holderId, Guid sectorId)
        {
            var getTicketsBySectorIdResponse = await Mediator.Send(new GetTicketsBySectorIdQuery { HolderId = holderId, SectorId = sectorId });

            if (!getTicketsBySectorIdResponse.Success)
                return BadRequest(getTicketsBySectorIdResponse.Message);

            var ticketDtos = new List<TicketDto>();

            if (getTicketsBySectorIdResponse.Tickets is not null && getTicketsBySectorIdResponse.Tickets.Any())
            {
                foreach (var ticket in getTicketsBySectorIdResponse.Tickets)
                {
                    var ticketDto = new TicketDto
                    {
                        Id = ticket.Id,
                        Title = ticket.Title,
                        Content = ticket.Content,
                        Status = ticket.Status,
                        Priority = ticket.Priority,
                        HolderId = ticket.HolderId,
                        SectorId = ticket.SectorId,
                        AssigneeId = ticket.AssigneeId,
                        UserId = ticket.UserId,
                        CreatedAt = ticket.CreatedAt,
                        UpdatedAt = ticket.UpdatedAt
                    };

                    ticketDtos.Add(ticketDto);
                }
            }

            return Ok(ticketDtos);
        }


        [HttpGet("holder/{holderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SectorDto>>> GetTicketsByHolderId(Guid holderId,
                                                                                    [FromQuery] string sector,
                                                                                    [FromQuery] string title,
                                                                                    [FromQuery] string status,
                                                                                    [FromQuery] string priority,
                                                                                    [FromQuery] int page,
                                                                                    [FromQuery] int pageSize
                                                                                 )
        {
            var getTicketsByHolderIdResponse = await Mediator.Send(new GetTicketsByHolderIdQuery
            {
                HolderId = holderId,
                Sector = sector?.Trim() ?? string.Empty,
                Title = title?.Trim() ?? string.Empty,
                Status = status?.Trim() ?? string.Empty,
                Priority = priority?.Trim() ?? string.Empty,
                PageNumber = page == 0 ? 1 : page,
                PageSize = pageSize == 0 ? 10 : pageSize
            });

            if (!getTicketsByHolderIdResponse.Success)
                return BadRequest(getTicketsByHolderIdResponse.Message);

            var ticketDtos = new List<TicketDto>();

            if (getTicketsByHolderIdResponse.Tickets is not null && getTicketsByHolderIdResponse.Tickets.Any())
            {
                foreach (var ticket in getTicketsByHolderIdResponse.Tickets)
                {
                    var ticketDto = new TicketDto
                    {
                        Id = ticket.Id,
                        Title = ticket.Title,
                        Content = ticket.Content,
                        Status = ticket.Status,
                        Priority = ticket.Priority,
                        HolderId = ticket.HolderId,
                        SectorId = ticket.SectorId,
                        AssigneeId = ticket.AssigneeId,
                        UserId = ticket.UserId,
                        CreatedAt = ticket.CreatedAt,
                        UpdatedAt = ticket.UpdatedAt
                    };

                    ticketDtos.Add(ticketDto);
                }
            }

            var paginatedDto = new PaginatedDto<TicketDto>
            {
                PageNumber = getTicketsByHolderIdResponse.PageNumber,
                TotalPages = getTicketsByHolderIdResponse.TotalPages,
                Items = ticketDtos
            };

            return Ok(paginatedDto);
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Put(Guid id, UpdateTicketCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete(Guid id)
        {
            await Mediator.Send(new DeleteTicketCommand { Id = id });

            return NoContent();
        }
    }
}
