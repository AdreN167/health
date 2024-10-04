using Health.Domain.Interfaces;

namespace Health.Domain.Models.Entities;

public class WorkoutEvent : IEntity
{
    public long Id { get; set; }
    public DateTime Date { get; set; }
    public double BurnedCalories { get; set; }
    public long WorkoutId { get; set; }
    public virtual Workout? Workout { get; set; }
}

