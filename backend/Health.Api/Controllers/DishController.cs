using MediatR;
using Microsoft.AspNetCore.Mvc;
using Health.Core.Features.Dishes.Dto;
using Health.Core.Features.Dishes.Queries.Get;
using Health.Core.Features.Dishes.Commands.Create;
using Health.Domain.Models.Response;
using Health.Core.Features.Dishes.Commands.Update;
using Health.Core.Features.Dishes.Commands.Delete;
using Health.Core.Features.Dishes.Queries.GetExtendedDishById;
using Microsoft.AspNetCore.Authorization;

namespace Health.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class DishController : ControllerBase
{
    private readonly IMediator _mediator;

    public DishController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("GetDishes")]
    public async Task<ActionResult<CollectionResponse<ExtendedDishDto>>> GetDishes()
    {
        var result = await _mediator.Send(new GetDishesQuery());
        return result.ISuccessful 
            ? Ok(result)
            : BadRequest(result);
    }

    [HttpGet("GetExtendedDishById/{id}")]
    public async Task<ActionResult<CollectionResponse<ExtendedDishDto>>> GetExtendedDishById(long id)
    {
        var result = await _mediator.Send(new GetExtendedDishByIdQuery(id));
        return result.ISuccessful
            ? Ok(result)
            : BadRequest(result);
    }

    //[Authorize(Roles = "Admin")]
    [HttpPost("CreateDish")]
    public async Task<ActionResult<BaseResponse<long>>> CreateDish([FromForm] CreateDishCommand request)
    {
        var result = await _mediator.Send(request);
        return result.ISuccessful
            ? Ok(result)
            : BadRequest(result);
    }

    //[Authorize(Roles = "Admin")]
    [HttpPut("UpdateDish")]
    public async Task<ActionResult<BaseResponse<long>>> UpdateDish([FromForm] UpdateDishCommand request)
    {
        var result = await _mediator.Send(request);
        return result.ISuccessful
            ? Ok(result)
            : BadRequest(result);
    }

    //[Authorize(Roles = "Admin")]
    [HttpDelete("DeleteDish/{id}")]
    public async Task<ActionResult<BaseResponse<long>>> DeleteDish(long id)
    {
        var result = await _mediator.Send(new DeleteDishCommand(id));
        return result.ISuccessful
            ? Ok(result)
            : BadRequest(result);
    }
}
