using Health.Core.Features.Products.Dto;

namespace Health.Core.Features.Dishes.Dto;

public class ExtendedDishDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Calories { get; set; }
    public double Fats { get; set; }
    public double Proteins { get; set; }
    public double Carbohydrates { get; set; }
    public string ImageUrl { get; set; }
    public ICollection<ProductDto> Products { get; set;}
}

