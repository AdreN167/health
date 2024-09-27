using Health.Domain.Interfaces;

namespace Health.Domain.Models.Entities;

public class Goal : IEntity
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public DateTime Deadline { get; set; }
    public long UserId { get; set; }
    public User User { get; set; }
}

