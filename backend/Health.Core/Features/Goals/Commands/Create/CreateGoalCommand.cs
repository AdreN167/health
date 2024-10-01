using Health.Domain.Models.Response;
using MediatR;

namespace Health.Core.Features.Goals.Commands.Create;

public class CreateGoalCommand : IRequest<BaseResponse<long>>
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string Deadline { get; set; }
    public string UserEmail { get; set; }
}

