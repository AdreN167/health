using Health.Core.Features.Dishes.Dto;
using Health.Core.Features.Products.Dto;

namespace Health.Core.Features.Diets.Dtos;

public class ExtendedDietDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public ICollection<ExtendedDishDto>? Dishes { get; set; }
    public ICollection<ProductDto>? Products { get; set; }
}

