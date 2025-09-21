namespace Health.Domain.Models.Entities;

public class DishProduct
{
    public long DishId { get; set; }
    public virtual Dish? Dish { get; set; }
    public long ProductId { get; set; }
    public virtual Product? Product { get; set; }
    public double Weight { get; set; }
}

