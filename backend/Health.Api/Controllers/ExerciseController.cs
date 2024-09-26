using Health.Core.Features.Exercises.Commands.Create;
using Health.Core.Features.Exercises.Commands.Delete;
using Health.Core.Features.Exercises.Commands.Update;
using Health.Core.Features.Exercises.Dtos;
using Health.Core.Features.Exercises.Queries.Get;
using Health.Domain.Models.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Health.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ExerciseController : ControllerBase
{
    private readonly IMediator _mediator;

    public ExerciseController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("GetExercises")]
    public async Task<ActionResult<CollectionResponse<ExerciseDto>>> GetExercises()
    {
        var result = await _mediator.Send(new GetExercisesQuery());
        return result.ISuccessful
            ? Ok(result)
            : BadRequest(result);
    }

    //[Authorize(Roles = "Admin")]
    [HttpPost("CreateExercise")]
    public async Task<ActionResult<BaseResponse<long>>> CreateExercise([FromBody] CreateExerciseCommand request)
    {
        var result = await _mediator.Send(request);
        return result.ISuccessful
            ? Ok(result)
            : BadRequest(result);
    }

    ////[Authorize(Roles = "Admin")]
    [HttpPut("UpdateExercise")]
    public async Task<ActionResult<BaseResponse<ExerciseDto>>> UpdateExercise([FromBody] UpdateExerciseCommand request)
    {
        var result = await _mediator.Send(request);
        return result.ISuccessful
            ? Ok(result.Data)
            : BadRequest(result);
    }

    //[Authorize(Roles = "Admin")]
    [HttpDelete("DeleteExercise/{id}")]
    public async Task<ActionResult<BaseResponse<ExerciseDto>>> DeleteExercise(long id)
    {
        var result = await _mediator.Send(new DeleteExerciseCommand(id));
        return result.ISuccessful
            ? Ok(result.Data)
            : BadRequest(result);
    }
}
