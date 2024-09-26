using Health.Core.Features.Trainers.Commands.Create;
using Health.Core.Features.Trainers.Commands.Delete;
using Health.Core.Features.Trainers.Commands.Update;
using Health.Core.Features.Trainers.Dtos;
using Health.Core.Features.Trainers.Queries.Get;  
using Health.Domain.Models.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Health.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TrainerController : ControllerBase
{
    private readonly IMediator _mediator;

    public TrainerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("GetTrainers")]
    public async Task<ActionResult<CollectionResponse<TrainerDto>>> GetTrainers()
    {
        var result = await _mediator.Send(new GetTrainersQuery());
        return result.ISuccessful
            ? Ok(result)
            : BadRequest(result);
    }

    //[Authorize(Roles = "Admin")]
    [HttpPost("CreateTrainer")]
    public async Task<ActionResult<BaseResponse<long>>> CreateTrainer([FromForm] CreateTrainerCommand request)
    {
        var result = await _mediator.Send(request);
        return result.ISuccessful
            ? Ok(result)
            : BadRequest(result);
    }

    //[Authorize(Roles = "Admin")]
    [HttpPut("UpdateTrainer")]
    public async Task<ActionResult<BaseResponse<TrainerDto>>> UpdateTrainer([FromForm] UpdateTrainerCommand request)
    {
        var result = await _mediator.Send(request);
        return result.ISuccessful
            ? Ok(result.Data)
            : BadRequest(result);
    }

    //[Authorize(Roles = "Admin")]
    [HttpDelete("DeleteTrainer/{id}")]
    public async Task<ActionResult<BaseResponse<TrainerDto>>> DeleteTrainer(long id)
    {
        var result = await _mediator.Send(new DeleteTrainerCommand(id));
        return result.ISuccessful
            ? Ok(result.Data)
            : BadRequest(result);
    }
}
