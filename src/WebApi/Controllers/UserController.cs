using Application.Common.Models;
using Application.UseCases.User.Commands.CreateUser;
using Application.UseCases.User.Commands.DeleteUser;
using Application.UseCases.User.Commands.UpdateUser;
using Application.UseCases.User.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class UserController : BaseController
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Guid?>> Post(CreateUserCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet("{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UserDto>> Get(string email)
        {
            var userEntity = await Mediator.Send(new GetUserByEmailQuery { Email = email });

            var userDto = userEntity == null ? null : new UserDto
            {
                Name = userEntity.Name,
                Email = userEntity.Email,
                Password = userEntity.Password
            };

            return userEntity == null ? NotFound() : Ok(userDto);
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
}
