using Health.Core.Features.Products.Commands.Delete;
using Health.Core.Features.Tokens.Commands.Refresh;
using Health.Core.Features.Tokens.Dtos;
using Health.Domain.Models.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Health.Api.Controllers;

[ApiController]
public class TokenController : Controller
{
    private readonly IMediator _mediator;

    public TokenController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("refreshToken")]
    public async Task<ActionResult<BaseResponse<TokenDto>>> RefreshToken([FromBody] RefreshTokenCommand request)
    {
        var result = await _mediator.Send(request);
        return result.ISuccessful
            ? Ok(result)
            : BadRequest(result);
    }
}
