namespace Health.Core.Features.Dishes.Dto;

public class DishDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public ICollection<string> Products { get; set; }
}

