using AutoMapper;
using Health.Core.Features.Workouts.Dtos;
using Health.Core.Resources;
using Health.DAL;
using Health.Domain.Models.Entities;
using Health.Domain.Models.Enums;
using Health.Domain.Models.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Health.Core.Features.Workouts.Command.AddExercises;

public class UpdateListOfExercisesCommandHandler(ApplicationDbContext context, IMapper mapper)
    : IRequestHandler<UpdateListOfExercisesCommand, BaseResponse<ExtendedWorkoutDto>>
{
    public async Task<BaseResponse<ExtendedWorkoutDto>> Handle(UpdateListOfExercisesCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.ExercisesWithRepetitions == null || request.ExercisesWithRepetitions.Count == 0)
            {
                return new BaseResponse<ExtendedWorkoutDto>
                {
                    ErrorCode = (int)ErrorCode.InvalidRequest,
                    ErrorMessage = ErrorMessages.InvalidRequest
                };
            }

            var workout = await context.Workouts.FindAsync([request.Id], cancellationToken);

            if (workout == null)
            {
                return new BaseResponse<ExtendedWorkoutDto>
                {
                    ErrorCode = (int)ErrorCode.WorkoutNotFound,
                    ErrorMessage = ErrorMessages.WorkoutNotFound
                };
            }

            var exercises = await context.Exercises.ToListAsync(cancellationToken);

            // хз, почему, но в одном запросе не пашет...
            exercises = exercises.Where(ex => request.ExercisesWithRepetitions.ContainsKey(ex.Id.ToString())).ToList();

            if (exercises.Count != request.ExercisesWithRepetitions.Count)
            {
                return new BaseResponse<ExtendedWorkoutDto>
                {
                    ErrorCode = (int)ErrorCode.ExerciseNotFound,
                    ErrorMessage = ErrorMessages.ExerciseNotFound
                };
            }

            workout.WorkoutExercise.Clear();

            foreach (var exercise in exercises)
            {
                workout.WorkoutExercise!.Add(new WorkoutExercise
                {
                    Exercise = exercise,
                    Repetitions = request.ExercisesWithRepetitions[exercise.Id.ToString()]
                });
            }

            await context.SaveChangesAsync(cancellationToken);

            return new BaseResponse<ExtendedWorkoutDto>
            {
                Data = mapper.Map<ExtendedWorkoutDto>(workout),
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<ExtendedWorkoutDto>
            {
                ErrorCode = (int)ErrorCode.InternalServerError,
                ErrorMessage = ErrorMessages.InternalServerError + " -> " + ex.Message
            };
        }
    }
}

