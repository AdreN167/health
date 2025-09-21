using Health.Domain.Models.Response;
using MediatR;

namespace Health.Core.Features.Diets.Commands.Create;

public class CreateDietCommand : IRequest<BaseResponse<long>>
{
    public long GoalId { get; set; }
    public string Name { get; set; }
}

