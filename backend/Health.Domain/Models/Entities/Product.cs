using Health.Domain.Interfaces;
using Health.Domain.Models.Abstract;

namespace Health.Domain.Models.Entities;

public class Product : Food, IEntity
{
    public long Id { get; set; }
    public string FileName { get; set; }
    public virtual ICollection<Dish>? Dishes { get; set; }

    public Product()
    {
        Name = string.Empty;
        Dishes = new List<Dish>();
    }
}

