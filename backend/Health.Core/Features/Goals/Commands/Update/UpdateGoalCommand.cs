using Health.Core.Features.Goals.Dtos;
using Health.Domain.Models.Response;
using MediatR;

namespace Health.Core.Features.Goals.Commands.Update;

public class UpdateGoalCommand : IRequest<BaseResponse<GoalDto>>
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string Deadline { get; set; }
}

