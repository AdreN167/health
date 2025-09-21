using Health.Domain.Models.Response;
using MediatR;

namespace Health.Core.Features.Workouts.Command.Create;

public class CreateWorkoutCommand : IRequest<BaseResponse<long>>
{
    public string Name { get; set; }
    public long GoalId { get; set; }
}

