namespace Health.Domain.Models.Entities;

// создаю отдельную сущность, чтобы доьавить в таблицу поле Повторения
public class WorkoutExercise
{
    public long WorkoutId { get; set; }
    public virtual Workout? Workout { get; set; }
    public long ExerciseId { get; set; }
    public virtual Exercise? Exercise { get; set; }
    public int Repetitions { get; set; }
}

