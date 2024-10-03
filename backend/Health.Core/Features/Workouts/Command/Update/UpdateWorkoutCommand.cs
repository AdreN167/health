using Health.Core.Features.Workouts.Dtos;
using Health.Domain.Models.Response;
using MediatR;

namespace Health.Core.Features.Workouts.Command.Update;

public class UpdateWorkoutCommand :IRequest<BaseResponse<WorkoutDto>>
{
    public long Id { get; set; }
    public string Name { get; set; }
}

