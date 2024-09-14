using Health.Domain.Interfaces;

namespace Health.Domain.Models.Entities;

public class Trainer : IEntity
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string PictureFileName { get; set; }
    public ICollection<Exercise> Exercises { get; set; }
}

