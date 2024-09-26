using Health.Domain.Interfaces;

namespace Health.Domain.Models.Entities;

public class Exercise : IEntity
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int CaloriesBurned { get; set; }
    public Trainer? Trainer { get; set; }
    public long? TrainerId { get; set; }
}