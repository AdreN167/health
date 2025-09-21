using Health.Core.Resources;
using Health.DAL;
using Health.Domain.Models.Entities;
using Health.Domain.Models.Enums;
using Health.Domain.Models.Response;
using MediatR;

namespace Health.Core.Features.WorkoutEventJournal.Commands.Create;

public class CreateWorkoutEventCommandHandler(ApplicationDbContext context)
    : IRequestHandler<CreateWorkoutEventCommand, BaseResponse<long>>
{
    public async Task<BaseResponse<long>> Handle(CreateWorkoutEventCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (!DateTime.TryParse(request.Date, out var date))
            {
                return new BaseResponse<long>
                {
                    ErrorCode = (int)ErrorCode.InvalidRequest,
                    ErrorMessage = ErrorMessages.InvalidRequest,
                };
            }

            var workout = await context.Workouts.FindAsync([request.WorkoutId], cancellationToken);

            if (workout == null)
            {
                return new BaseResponse<long>
                {
                    ErrorCode = (int)ErrorCode.WorkoutNotFound,
                    ErrorMessage = ErrorMessages.WorkoutNotFound
                };
            }

            var newEvent = new WorkoutEvent
            {
                Date = date,
                Workout = workout,
                BurnedCalories = workout.WorkoutExercise.Sum(x => x.Repetitions * x.Exercise.CaloriesBurned)
            };

            await context.WorkoutEvents.AddAsync(newEvent, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return new BaseResponse<long>
            {
                Data = newEvent.Id
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<long>
            {
                ErrorCode = (int)ErrorCode.InternalServerError,
                ErrorMessage = ErrorMessages.InternalServerError + " -> " + ex.Message
            };
        }
    }
}

