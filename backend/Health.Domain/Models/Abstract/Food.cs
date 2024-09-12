namespace Health.Domain.Models.Abstract;

public abstract class Food
{
    public string Name { get; set; }
    public int Calories { get; set; }
    public int Fats { get; set; }
    public int Proteins { get; set; }
    public int Carbohydrates { get; set; }
}

