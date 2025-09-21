namespace Health.Domain.Models.Entities;

public class DietProduct
{
    public long ProductId { get; set; }
    public virtual Product? Product { get; set; }
    public long DietId { get; set; }
    public virtual Diet? Diet { get; set; }
    public double Weight { get; set; }
}

