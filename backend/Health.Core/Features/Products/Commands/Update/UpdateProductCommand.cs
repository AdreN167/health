using Health.Core.Features.Products.Dto;
using Health.Domain.Models.Response;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Health.Core.Features.Products.Commands.Update;

public class UpdateProductCommand : IRequest<BaseResponse<ProductDto>>
{
    public long Id { get; set; }
    public string Name { get; set; }
    public double Calories { get; set; }
    public double Fats { get; set; }
    public double Proteins { get; set; }
    public double Carbohydrates { get; set; }
    public IFormFile? Image { get; set; }
}

