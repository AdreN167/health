namespace Health.Core.Features.Workouts.Dtos;

public class ExtendedWorkoutDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public ICollection<WorkoutExerciseDto>? Exercises { get; set; }
}

