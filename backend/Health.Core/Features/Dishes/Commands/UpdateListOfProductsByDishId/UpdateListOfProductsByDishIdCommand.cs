using Health.Core.Features.Dishes.Dto;
using Health.Domain.Models.Response;
using MediatR;

namespace Health.Core.Features.Dishes.Commands.UpdateListOfProductsByDishId;

public class UpdateListOfProductsByDishIdCommand : IRequest<BaseResponse<ExtendedDishDto>>
{
    public long Id { get; set; }
    public Dictionary<string, double> ProductsWithWeight { get; set; }
}

