using Application.UseCases.SignUp.Commands.CreateHolderAndUser;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class SignUpController : BaseController
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CreateHolderAndUserResponse>> Post(CreateHolderAndUserCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
