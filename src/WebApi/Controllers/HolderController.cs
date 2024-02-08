using Application.Common.Models;
using Application.UseCases.Holder.Commands.CreateHolder;
using Application.UseCases.Holder.Commands.DeleteHolder;
using Application.UseCases.Holder.Commands.UpdateHolder;
using Application.UseCases.Holder.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class HolderController : BaseController
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CreateHolderResponse>> Post(CreateHolderCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<HolderDto>> Get(Guid id)
        {
            var getHolderByIdResponse = await Mediator.Send(new GetHolderByIdQuery { Id = id });

            if (getHolderByIdResponse.Holder is null)
                return NotFound();

            if (!getHolderByIdResponse.Success)
                return BadRequest(getHolderByIdResponse.Message);

            var holderDto = new HolderDto
            {
                Name = getHolderByIdResponse.Holder.Name,
                Sectors = getHolderByIdResponse.Holder.Sectors
            };

            return Ok(holderDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Put(Guid id, UpdateHolderCommand command)
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
            await Mediator.Send(new DeleteHolderCommand { Id = id });

            return NoContent();
        }
    }
}
