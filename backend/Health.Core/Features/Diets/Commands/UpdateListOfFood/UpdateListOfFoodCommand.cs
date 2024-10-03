using Health.Core.Features.Diets.Dtos;
using Health.Domain.Models.Response;
using MediatR;

namespace Health.Core.Features.Diets.Commands.UpdateListOfFood;

public class UpdateListOfFoodCommand : IRequest<BaseResponse<ExtendedDietDto>>
{
    public long Id { get; set; }
    public ICollection<long> ProductIds { get; set; }
    public ICollection<long> DishIds { get; set; }
}

