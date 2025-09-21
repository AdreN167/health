namespace Health.Core.Features.Products.Dto;

public class ProductDto()
{
    public long Id { get; set; }
    public string Name { get; set; }
    public double Calories { get; set; }
    public double Fats { get; set; }
    public double Proteins { get; set; }
    public double Carbohydrates { get; set; }
    public string ImageUrl { get; set; }

    //public ICollection<Dish> Dishes { get; set; } // пока хз, надо ли возвращать в продукте блюда, в которых он используется
}

