using Health.Core.Features.Products.Dto;
using Health.Domain.Models.Response;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Health.Core.Features.Products.Commands.Update;

public class UpdateProductCommand : IRequest<BaseResponse<ProductDto>>
{
    public long Id { get; set; }
    public string Name { get; set; }
    public int Calories { get; set; }
    public int Fats { get; set; }
    public int Proteins { get; set; }
    public int Carbohydrates { get; set; }
    public IFormFile? Image { get; set; }
}

