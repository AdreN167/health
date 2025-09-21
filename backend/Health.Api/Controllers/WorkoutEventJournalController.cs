using Health.Core.Features.WorkoutEventJournal.Commands.Create;
using Health.Core.Features.WorkoutEventJournal.Dtos;
using Health.Core.Features.WorkoutEventJournal.Queries.Get;
using Health.Core.Features.WorkoutEventJournal.Queries.GetByGoalId;
using Health.Domain.Models.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Health.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class WorkoutEventJournalController : ControllerBase
{
    private readonly IMediator _mediator;

    public WorkoutEventJournalController(IMediator mediator)
    {
        _mediator = mediator;
    }

    //[Authorize(Roles = "User")]
    [HttpGet("{email}")]
    public async Task<ActionResult<CollectionResponse<WorkoutEventDto>>> GetWorkoutEvents(string email)
    {
        var result = await _mediator.Send(new GetWorkoutEventsQuery(email));
        return result.ISuccessful
            ? Ok(result)
            : BadRequest(result);
    }

    //[Authorize(Roles = "User")]
    [HttpGet]
    public async Task<ActionResult<CollectionResponse<WorkoutEventDto>>> GetWorkoutEventsByGoalId(string email, long goalId)
    {
        var result = await _mediator.Send(new GetWorkoutEventsByGoalIdQuery(email, goalId));
        return result.ISuccessful
            ? Ok(result)
            : BadRequest(result);
    }

    //[Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<BaseResponse<long>>> CreateWorkoutEvent([FromBody] CreateWorkoutEventCommand request)
    {
        var result = await _mediator.Send(request);
        return result.ISuccessful
            ? Ok(result)
            : BadRequest(result);
    }
}
