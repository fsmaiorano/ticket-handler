using Application.UseCases.User.Commands.CreateUser;
using Microsoft.AspNetCore.Mvc;
using Application.Common.Exceptions;

namespace WebApi.Controllers
{
    public class UserController : BaseController
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Guid?>> Create(CreateUserCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
