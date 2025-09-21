using Health.Core.Features.Diets.Commands.Create;
using Health.Core.Features.Diets.Commands.Delete;
using Health.Core.Features.Diets.Commands.Update;
using Health.Core.Features.Diets.Commands.UpdateListOfFood;
using Health.Core.Features.Diets.Dtos;
using Health.Core.Features.Diets.Queries.GetDietsByGoalId;
using Health.Domain.Models.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Health.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class DietController : ControllerBase
{
    private readonly IMediator _mediator;

    public DietController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("byGoal/{id}")]
    public async Task<ActionResult<CollectionResponse<ExtendedDietDto>>> GetDietsByGoalId(long id)
    {
        var result = await _mediator.Send(new GetDietsByGoalIdQuery(id));
        return result.ISuccessful
            ? Ok(result)
            : BadRequest(result);
    }

    //[Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<BaseResponse<long>>> CreateDiet([FromBody] CreateDietCommand request)
    {
        var result = await _mediator.Send(request);
        return result.ISuccessful
            ? Ok(result)
            : BadRequest(result);
    }

    //[Authorize(Roles = "Admin")]
    [HttpPut]
    public async Task<ActionResult<BaseResponse<DietDto>>> UpdateDiet([FromBody] UpdateDietCommand request)
    {
        var result = await _mediator.Send(request);
        return result.ISuccessful
            ? Ok(result)
            : BadRequest(result);
    }

    [HttpPut("update/updateListOfFood")]
    public async Task<ActionResult<BaseResponse<DietDto>>> UpdateListOfExercisesToDiet([FromBody] UpdateListOfFoodCommand request)
    {
        var result = await _mediator.Send(request);
        return result.ISuccessful
            ? Ok(result)
            : BadRequest(result);
    }

    //[Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<ActionResult<BaseResponse<DietDto>>> DeleteDiet(long id)
    {
        var result = await _mediator.Send(new DeleteDietCommand(id));
        return result.ISuccessful
            ? Ok(result)
            : BadRequest(result);
    }
}
