using Health.Core.Features.Diets.Dtos;
using Health.Core.Features.Workouts.Dtos;

namespace Health.Core.Features.Goals.Dtos;

public class ExtendedGoalDto : GoalDto
{
    public ICollection<DietDto>? Diets { get; set; }
    public ICollection<WorkoutDto>? Workouts { get; set; }
}

