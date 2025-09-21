namespace Health.Domain.Models.Entities;

public class DietDish
{
    public long DishId { get; set; }
    public virtual Dish? Dish { get; set; }
    public long DietId { get; set; }
    public virtual Diet? Diet { get; set; }
    public double Weight { get; set; }
}

