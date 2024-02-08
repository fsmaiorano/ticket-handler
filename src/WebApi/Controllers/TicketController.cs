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
                Title = getTicketByIdResponse.Ticket.Title,
                Content = getTicketByIdResponse.Ticket.Content,
                Status = getTicketByIdResponse.Ticket.Status,
                Priority = getTicketByIdResponse.Ticket.Priority,
                HolderId = getTicketByIdResponse.Ticket.HolderId,
                SectorId = getTicketByIdResponse.Ticket.SectorId,
                AssigneeId = getTicketByIdResponse.Ticket.AssigneeId
            };

            return Ok(ticketDto);
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
