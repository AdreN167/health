using Health.Core.Resources;
using Health.DAL;
using Health.Domain.Models.Entities;
using Health.Domain.Models.Enums;
using Health.Domain.Models.Response;
using MediatR;

namespace Health.Core.Features.Workouts.Command.Create;

public class CreateWorkoutCommandHandler(ApplicationDbContext context)
    : IRequestHandler<CreateWorkoutCommand, BaseResponse<long>>
{
    public async Task<BaseResponse<long>> Handle(CreateWorkoutCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return new BaseResponse<long>
                {
                    ErrorCode = (int)ErrorCode.InvalidRequest,
                    ErrorMessage = ErrorMessages.InvalidRequest
                };
            }

            var goal = await context.Goals.FindAsync([request.GoalId], cancellationToken);

            if (goal == null)
            {
                return new BaseResponse<long>
                {
                    ErrorCode = (int)ErrorCode.GoalNotFound,
                    ErrorMessage = ErrorMessages.GoalNotFound
                };
            }

            var newWorkout = new Workout
            {
                Name = request.Name,
                Goal = goal
            };

            await context.Workouts.AddAsync(newWorkout, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return new BaseResponse<long>
            {
                Data = newWorkout.Id
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

