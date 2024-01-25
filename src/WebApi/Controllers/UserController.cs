using Application.UseCases.User.Commands.CreateUser;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class UserController : BaseController
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<int>> Create(CreateUserCommand command)
        {
            //return Ok(await Mediator.Send(command));
            var x = await Mediator.Send(command);
            return Ok(x);
        }
    }
}
