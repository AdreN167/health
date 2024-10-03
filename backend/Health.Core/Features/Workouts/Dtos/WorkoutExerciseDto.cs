using Health.Core.Features.Exercises.Dtos;

namespace Health.Core.Features.Workouts.Dtos;

public class WorkoutExerciseDto
{
    public int Repetitions { get; set; }
    public ExerciseDto? Exercise { get; set; }
}

