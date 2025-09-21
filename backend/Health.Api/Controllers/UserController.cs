using Health.Core.Features.Users.Commands.UpdateUsersInfo;
using Health.Core.Features.Users.Dtos;
using Health.Core.Features.Users.Queries.GetUsersInfo;
using Health.Domain.Models.Response;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace Health.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : Controller
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<BaseResponse<UsersInfoDto>>> GetUsersInfo(string email)
    {
        var result = await _mediator.Send(new GetUsersInfoQuery(email));
        return result.ISuccessful
            ? Ok(result)
            : BadRequest(result);
    }

    [HttpPut]
    public async Task<ActionResult<BaseResponse<UsersInfoDto>>> UpdateUsersInfo([FromBody] UpdateUsersInfoCommand request)
    {
        var result = await _mediator.Send(request);
        return result.ISuccessful
            ? Ok(result)
            : BadRequest(result);
    }
}
