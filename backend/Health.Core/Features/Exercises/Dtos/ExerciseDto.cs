using Health.Core.Features.Trainers.Dtos;

namespace Health.Core.Features.Exercises.Dtos;

public class ExerciseDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int CaloriesBurned { get; set; }
    public TrainerDto? Trainer { get; set; }
}

