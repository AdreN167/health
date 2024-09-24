using Health.Core.Features.Products.Dto;

namespace Health.Core.Features.Dishes.Dto;

public class ExtendedDishDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Calories { get; set; }
    public int Fats { get; set; }
    public int Proteins { get; set; }
    public int Carbohydrates { get; set; }
    public string ImageUrl { get; set; }
    public ICollection<ProductDto> Products { get; set;}
}

