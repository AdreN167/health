using Health.Domain.Interfaces;

namespace Health.Domain.Models.Entities;

public class DietEvent : IEntity
{
    public long Id { get; set; }
    public DateTime Date { get; set; }
    public double Calories { get; set; }
    public double Fats { get; set; }
    public double Proteins { get; set; }
    public double Carbohydrates { get; set; }
    public long DietId {  get; set; }
    public virtual Diet? Diet { get; set; }
}

