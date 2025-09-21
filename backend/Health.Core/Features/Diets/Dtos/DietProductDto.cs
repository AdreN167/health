namespace Health.Core.Features.Diets.Dtos;

public class DietProductDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public double Weight { get; set; }
    public double Calories { get; set; }
    public double Fats { get; set; }
    public double Proteins { get; set; }
    public double Carbohydrates { get; set; }
    public string ImageUrl { get; set; }
}