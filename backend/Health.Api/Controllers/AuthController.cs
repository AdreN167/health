using Health.Core.Features.Authentication.Commands.CreateAdminUser;
using Health.Core.Features.Authentication.Commands.Login;
using Health.Core.Features.Authentication.Commands.Registration;
using Health.Core.Features.Authentication.Dtos;
using Health.Domain.Models.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Health.Api.Controllers;
public class AuthController : Controller
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("signIn")]
    public async Task<ActionResult<BaseResponse<LoginDto>>> SignIn([FromBody] LoginCommand request)
    {
        var result = await _mediator.Send(request);
        return result.ISuccessful
            ? Ok(result)
            : BadRequest(result);
    }

    [HttpPost]
    [Route("signUp")]
    public async Task<ActionResult<BaseResponse<RegistrationDto>>> SignUp([FromBody] RegistrationCommand request)
    {
        var result = await _mediator.Send(request);
        return result.ISuccessful
            ? Ok(result)
            : BadRequest(result);
    }

    [HttpPost]
    [Route("createAdminUser")]
    public async Task<ActionResult<BaseResponse<RegistrationDto>>> CreateAdminUser([FromBody] CreateAdminUserCommand request)
    {
        var result = await _mediator.Send(request);
        return result.ISuccessful
            ? Ok(result)
            : BadRequest(result);
    }
}
