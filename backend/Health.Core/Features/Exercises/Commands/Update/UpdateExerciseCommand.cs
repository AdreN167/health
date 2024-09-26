using Health.Core.Features.Exercises.Dtos;
using Health.Domain.Models.Response;
using MediatR;

namespace Health.Core.Features.Exercises.Commands.Update;

public class UpdateExerciseCommand : IRequest<BaseResponse<ExerciseDto>>
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int CaloriesBurned { get; set; }
    public long? TrainerId { get; set; }
}

