using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;

namespace WebApi.Controllers;

[ApiController]
[ApiExceptionFilter]
[Route("api/[controller]")]
public abstract class BaseController : ControllerBase
{
    private ISender? _mediator;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    public BaseController()
    {

    }

    // public string ResponseBadRequest(string message)
    // {
    //     return JsonConvert.SerializeObject(new BadRequestError() { Message = message });
    // }
}
