using Health.Core.Features.Exercises.Dtos;
using Health.Core.Features.Trainers.Dtos;

namespace Health.Core.Features.Workouts.Dtos;

public class WorkoutExerciseDto
{
    public int Repetitions { get; set; }
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int CaloriesBurned { get; set; }
    public TrainerDto? Trainer { get; set; }
}

