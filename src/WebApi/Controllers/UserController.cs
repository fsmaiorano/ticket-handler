using Application.Common.Models;
using Application.UseCases.User.Commands.CreateUser;
using Application.UseCases.User.Commands.DeleteUser;
using Application.UseCases.User.Commands.UpdateUser;
using Application.UseCases.User.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class UserController : BaseController
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<CreateUserResponse>> Post(CreateUserCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpGet("{email}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<UserDto>> Get(string email)
    {
        var getUserByEmailResponse = await Mediator.Send(new GetUserByEmailQuery { Email = email });

        if (getUserByEmailResponse.User is null)
            return NotFound();

        if (!getUserByEmailResponse.Success)
            return BadRequest(getUserByEmailResponse.Message);

        var userDto = new UserDto
        {
            Name = getUserByEmailResponse.User.Name,
            Email = getUserByEmailResponse.User.Email,
            Role = getUserByEmailResponse.User.Role
        };

        return Ok(userDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Put(Guid id, UpdateUserCommand command)
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
        await Mediator.Send(new DeleteUserCommand { Id = id });
        return NoContent();
    }
}
