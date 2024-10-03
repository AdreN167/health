using Health.Domain.Interfaces;

namespace Health.Domain.Models.Entities;

public class Workout : IEntity
{
    public long Id { get; set; }
    public string Name { get; set; }
    public long GoalId {  get; set; }
    public virtual Goal? Goal { get; set; }
    public virtual ICollection<Exercise>? Exercises { get; set; }
    public virtual ICollection<WorkoutExercise>? WorkoutExercise { get; set; }
}

