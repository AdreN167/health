using Health.Core.Features.Workouts.Command.AddExercises;
using Health.Core.Features.Workouts.Command.Create;
using Health.Core.Features.Workouts.Command.Delete;
using Health.Core.Features.Workouts.Command.Update;
using Health.Core.Features.Workouts.Dtos;
using Health.Core.Features.Workouts.Queries.GetWorkoutsByGoalId;
using Health.Domain.Models.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Health.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class WorkoutController : ControllerBase
{
    private readonly IMediator _mediator;

    public WorkoutController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("byGoal/{id}")]
    public async Task<ActionResult<CollectionResponse<ExtendedWorkoutDto>>> GetWorkoutsByGoalId(long id)
    {
        var result = await _mediator.Send(new GetWorkoutsByGoalIdQuery(id));
        return result.ISuccessful
            ? Ok(result)
            : BadRequest(result);
    }

    //[Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<BaseResponse<long>>> CreateWorkout([FromBody] CreateWorkoutCommand request)
    {
        var result = await _mediator.Send(request);
        return result.ISuccessful
            ? Ok(result)
            : BadRequest(result);
    }

    //[Authorize(Roles = "Admin")]
    [HttpPut]
    public async Task<ActionResult<BaseResponse<WorkoutDto>>> UpdateWorkout([FromBody] UpdateWorkoutCommand request)
    {
        var result = await _mediator.Send(request);
        return result.ISuccessful
            ? Ok(result)
            : BadRequest(result);
    }

    [HttpPut("update/updateListOfExercises")]
    public async Task<ActionResult<BaseResponse<WorkoutDto>>> UpdateListOfExercisesToWorkout([FromBody] UpdateListOfExercisesCommand request)
    {
        var result = await _mediator.Send(request);
        return result.ISuccessful
            ? Ok(result)
            : BadRequest(result);
    }

    //[Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<ActionResult<BaseResponse<WorkoutDto>>> DeleteWorkout(long id)
    {
        var result = await _mediator.Send(new DeleteWorkoutCommand(id));
        return result.ISuccessful
            ? Ok(result)
            : BadRequest(result);
    }
}
