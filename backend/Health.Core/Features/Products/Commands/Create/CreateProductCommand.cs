using Health.Domain.Models.Response;
using MediatR;

namespace Health.Core.Features.Products.Commands.Create;

public class CreateProductCommand : IRequest<BaseResponse<long>> // вернет Id созданной сущности
{
    public string Name { get; set; }
    public int Calories { get; set; }
    public int Fats { get; set; }
    public int Proteins { get; set; }
    public int Carbohydrates { get; set; }
}

