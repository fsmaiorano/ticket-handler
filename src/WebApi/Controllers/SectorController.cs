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
        public async Task<ActionResult<CreateSectorResponse>> Post(CreateSectorCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<SectorDto>> Get(Guid id)
        {
            var getSectorByIdResponse = await Mediator.Send(new GetSectorByIdQuery { Id = id });

            if (getSectorByIdResponse.Sector is null)
                return NotFound();

            if (!getSectorByIdResponse.Success)
                return BadRequest(getSectorByIdResponse.Message);

            var sectorDto = new SectorDto
            {
                Name = getSectorByIdResponse.Sector.Name,
                HolderId = getSectorByIdResponse.Sector.HolderId,
                Users = getSectorByIdResponse.Sector.Users
            };

            return Ok(sectorDto);
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
