namespace Health.Core.Features.Products.Dto;

public class ProductDto()
{
    public long Id { get; set; }
    public string Name { get; set; }
    public int Calories { get; set; }
    public int Fats { get; set; }
    public int Proteins { get; set; }
    public int Carbohydrates { get; set; }
    public string ImageUrl { get; set; }

    //public ICollection<Dish> Dishes { get; set; } // пока хз, надо ли возвращать в продукте блюда, в которых он используется
}

