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
        public async Task<ActionResult<Guid?>> Post(CreateTicketCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<TicketDto>> Get(Guid id)
        {
            var ticketEntity = await Mediator.Send(new GetTicketByIdQuery { Id = id });

            var ticketDto = ticketEntity == null ? null : new TicketDto
            {
                Title = ticketEntity.Title,
                Content = ticketEntity.Content,
                Status = ticketEntity.Status,
                Priority = ticketEntity.Priority,
                HolderId = ticketEntity.HolderId,
                SectorId = ticketEntity.SectorId,
                AssigneeId = ticketEntity.AssigneeId
            };

            return ticketEntity == null ? NotFound() : Ok(ticketDto);
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
