using Application.Common.Models;
using Application.UseCases.Sector.Commands.CreateSector;
using Application.UseCases.Sector.Commands.UpdateSector;
using Application.UseCases.Sector.Queries;
using Application.UseCases.User.Commands.SectorUser;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class SectorController : BaseController
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Guid?>> Post(CreateSectorCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<SectorDto>> Get(Guid id)
        {
            var sectorEntity = await Mediator.Send(new GetSectorByIdQuery { Id = id });

            var sectorDto = sectorEntity == null ? null : new SectorDto
            {
                Name = sectorEntity.Name,
                HolderEntity = sectorEntity.HolderEntity,
                Users = sectorEntity.Users
            };

            return sectorEntity == null ? NotFound() : Ok(sectorDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Put(Guid id, UpdateSectorCommand command)
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
            await Mediator.Send(new DeleteSectorCommand { Id = id });

            return NoContent();
        }
    }
}
