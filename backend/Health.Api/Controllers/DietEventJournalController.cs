using Health.Core.Features.DietEventJournal.Commands.Create;
using Health.Core.Features.DietEventJournal.Dtos;
using Health.Core.Features.DietEventJournal.Queries.Get;
using Health.Domain.Models.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Health.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class DietEventJournalController : ControllerBase
{
    private readonly IMediator _mediator;

    public DietEventJournalController(IMediator mediator)
    {
        _mediator = mediator;
    }

    //[Authorize(Roles = "User")]
    [HttpGet]
    public async Task<ActionResult<CollectionResponse<DietEventDto>>> GetDietEvents()
    {
        var result = await _mediator.Send(new GetDietEventsQuery());
        return result.ISuccessful
            ? Ok(result)
            : BadRequest(result);
    }

    //[Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<BaseResponse<long>>> CreateDietEvent([FromBody] CreateDietEventCommand request)
    {
        var result = await _mediator.Send(request);
        return result.ISuccessful
            ? Ok(result)
            : BadRequest(result);
    }
}
