using Health.Domain.Interfaces;

namespace Health.Domain.Models.Entities;

public class Product : IEntity
{
    public long Id { get; set; }
    public string Name { get; set; }
    public double Calories { get; set; }
    public double Fats { get; set; }
    public double Proteins { get; set; }
    public double Carbohydrates { get; set; }
    public string FileName { get; set; }
    public virtual ICollection<Dish>? Dishes { get; set; }
    public virtual ICollection<Diet>? Diets { get; set; }
}

