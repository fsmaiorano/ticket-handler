using Application.UseCases.Authentication.Commands.SignUp;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class AuthenticationController : BaseController
{
    [HttpPost("signup")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<SignUpResponse>> Post(SignUpCommand command)
    {
        return await Mediator.Send(command);
    }

    //[HttpPost("signin")]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //public async Task<ActionResult<string>> Post(CreateHolderAndUserCommand command)
    //{
    //    return await Mediator.Send(command);
    //}
}
