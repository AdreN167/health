using Health.Domain.Interfaces;

namespace Health.Domain.Models.Entities;

public class Dish : IEntity
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string FileName { get; set; }
    public virtual ICollection<Product> Products { get; set; }

    public Dish()
    {
        Name = string.Empty;
        Description = string.Empty;
        Products = new List<Product>();
    }
}

