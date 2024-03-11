﻿using Application.Common.Models;
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

        [HttpGet("holder/{holderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SectorDto>>> GetSectorsByHolderId(Guid holderId)
        {
            var getSectorsByHolderIdResponse = await Mediator.Send(new GetSectorsByHolderIdQuery { HolderId = holderId });

            if (!getSectorsByHolderIdResponse.Success)
                return BadRequest(getSectorsByHolderIdResponse.Message);

            var sectorDtos = new List<SectorDto>();

            if (getSectorsByHolderIdResponse.Sectors is not null && getSectorsByHolderIdResponse.Sectors.Any())
            {
                foreach (var sector in getSectorsByHolderIdResponse.Sectors)
                {
                    sectorDtos.Add(new SectorDto
                    {
                        Id = sector.Id,
                        Name = sector.Name,
                        HolderId = sector.HolderId,
                        IsActive = sector.IsActive,
                        CreatedAt = sector.CreatedAt,
                        UpdatedAt = sector.UpdatedAt
                    });
                }
            }

            return Ok(sectorDtos);
        }
    }
}
