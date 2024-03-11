using Application.UseCases.Authentication.Commands.SignIn;
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

    [HttpPost("signin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<SignInResponse>> Post(SignInCommand command)
    {
        var signInResponse = await Mediator.Send(command);

        if (signInResponse.User is null)
            return NotFound();

        if (!signInResponse.Success)
            return BadRequest(signInResponse.Message);
        
        return Ok(signInResponse);
    }
}
