using Application.UseCases.SignUp.Commands.CreateHolderAndUser;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;

namespace WebApi.Controllers;

public class AuthenticationController : BaseController
{
    [HttpPost("signup")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<CreateHolderAndUserResponse>> Post(CreateHolderAndUserCommand command)
    {
        return await Mediator.Send(command);
    }
}
