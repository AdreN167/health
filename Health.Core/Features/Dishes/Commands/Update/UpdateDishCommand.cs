using Health.Core.Features.Dishes.Dto;
using Health.Domain.Models.Response;
using MediatR;

namespace Health.Core.Features.Dishes.Commands.Update;

public class UpdateDishCommand : IRequest<BaseResponse<DishDto>>
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<long> ProductIds { get; set; }
}

