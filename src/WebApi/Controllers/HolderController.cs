using Application.Common.Models;
using Application.UseCases.Holder.Commands.CreateHolder;
using Application.UseCases.Holder.Commands.DeleteHolder;
using Application.UseCases.Holder.Commands.UpdateHolder;
using Application.UseCases.Holder.Queries;
using Application.UseCases.User.Queries;
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
                Id = getHolderByIdResponse.Holder.Id,
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

        [HttpGet("{id}/users")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UserDto>> GetUsersByHolderId(Guid id)
        {
            var getUsersByHolderIdResponse = await Mediator.Send(new GetUsersByHolderIdQuery { HolderId = id });

            if (getUsersByHolderIdResponse.Users is null)
                return NotFound();

            if (!getUsersByHolderIdResponse.Success)
                return BadRequest(getUsersByHolderIdResponse.Message);

            var userDtos = getUsersByHolderIdResponse.Users.Select(user => new UserDto
            {
                Name = user.Name,
                Email = user.Email,
                Role = user.Role
            });

            return Ok(userDtos);
        }
    }
}
