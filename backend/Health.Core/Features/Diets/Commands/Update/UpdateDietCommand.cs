using Health.Core.Features.Diets.Dtos;
using Health.Domain.Models.Response;
using MediatR;

namespace Health.Core.Features.Diets.Commands.Update;

public class UpdateDietCommand : IRequest<BaseResponse<DietDto>>
{
    public long Id { get; set; }
    public string Name { get; set; }
}

