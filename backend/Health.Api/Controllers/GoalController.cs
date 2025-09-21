using Health.Core.Features.Goals.Commands.Create;
using Health.Core.Features.Goals.Commands.Delete;
using Health.Core.Features.Goals.Commands.Update;
using Health.Core.Features.Goals.Dtos;
using Health.Core.Features.Goals.Queries.Get;
using Health.Core.Features.Goals.Queries.GetExtendedGoalById;
using Health.Core.Features.Goals.Queries.GetGoalsByUserEmail;
using Health.Domain.Models.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Health.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class GoalController : ControllerBase
{
    private readonly IMediator _mediator;

    public GoalController(IMediator mediator)
    {
        _mediator = mediator;
    }

    //[Authorize(Roles = "User")]
    [HttpGet]
    public async Task<ActionResult<CollectionResponse<GoalDto>>> GetGoals()
    {
        var result = await _mediator.Send(new GetGoalsQuery());
        return result.ISuccessful
            ? Ok(result)
            : BadRequest(result);
    }

    //[Authorize(Roles = "User")]
    [HttpGet("email/{email}")]
    public async Task<ActionResult<CollectionResponse<GoalDto>>> GetGoalsByUserEmail(string email)
    {
        var result = await _mediator.Send(new GetGoalsByUserEmailQuery(email));
        return result.ISuccessful
            ? Ok(result)
            : BadRequest(result);
    }

    //[Authorize(Roles = "User")]
    [HttpGet("{id}")]
    public async Task<ActionResult<CollectionResponse<ExtendedGoalDto>>> GetExtendedGoal(long id)
    {
        var result = await _mediator.Send(new GetExtendedGoalByIdQuery(id));
        return result.ISuccessful
            ? Ok(result)
            : BadRequest(result);
    }

    //[Authorize(Roles = "User")]
    [HttpPost]
    public async Task<ActionResult<BaseResponse<long>>> CreateGoal([FromBody] CreateGoalCommand request)
    {
        var result = await _mediator.Send(request);
        return result.ISuccessful
            ? Ok(result)
            : BadRequest(result);
    }

    //[Authorize(Roles = "User")]
    [HttpPut]
    public async Task<ActionResult<BaseResponse<GoalDto>>> UpdateGoal([FromBody] UpdateGoalCommand request)
    {
        var result = await _mediator.Send(request);
        return result.ISuccessful
            ? Ok(result)
            : BadRequest(result);
    }

    //[Authorize(Roles = "User")]
    [HttpDelete("{id}")]
    public async Task<ActionResult<BaseResponse<GoalDto>>> DeleteGoal(long id)
    {
        var result = await _mediator.Send(new DeleteGoalCommand(id));
        return result.ISuccessful
            ? Ok(result)
            : BadRequest(result);
    }
}
