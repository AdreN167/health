using Health.Domain.Models.Response;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Health.Core.Features.Products.Commands.Create;

public class CreateProductCommand : IRequest<BaseResponse<long>> // вернет Id созданной сущности
{
    public string Name { get; set; }
    public double Calories { get; set; }
    public double Fats { get; set; }
    public double Proteins { get; set; }
    public double Carbohydrates { get; set; }
    public IFormFile? Image { get; set; }
}

