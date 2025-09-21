namespace Health.Core.Features.Diets.Dtos;

public class ExtendedDietDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public ICollection<DietDishDto>? Dishes { get; set; }
    public ICollection<DietProductDto>? Products { get; set; }
}

