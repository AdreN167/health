using Health.Domain.Models.Response;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Health.Core.Features.Dishes.Commands.Create;

public class CreateDishCommand : IRequest<BaseResponse<long>>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public IFormFile? Image { get; set; }
}

