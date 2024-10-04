using Health.Domain.Interfaces;

namespace Health.Domain.Models.Entities;

public class Diet : IEntity
{
    public long Id {  get; set; }
    public string Name { get; set; }
    public long GoalId { get; set; }
    public virtual Goal? Goal { get; set; }
    public virtual ICollection<Dish>? Dishes { get; set; }
    public virtual ICollection<Product>? Products { get; set; }
    public virtual ICollection<DietEvent>? EventJournal { get; set; }
}

