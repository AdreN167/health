using Health.Domain.Models.Response;
using MediatR;

namespace Health.Core.Features.Exercises.Commands.Create;

public class CreateExerciseCommand : IRequest<BaseResponse<long>>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int CaloriesBurned { get; set; }
    public long? TrainerId { get; set; }
}

