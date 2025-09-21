using Health.Core.Features.Workouts.Dtos;
using Health.Domain.Models.Response;
using MediatR;

namespace Health.Core.Features.Workouts.Command.AddExercises;

public class UpdateListOfExercisesCommand : IRequest<BaseResponse<ExtendedWorkoutDto>>
{
    public long Id { get; set; }
    public Dictionary<string, int> ExercisesWithRepetitions { get; set; } // string => id упражнения, парсинг внутри хендлера
}

