using AutoMapper;
using Health.Core.Features.Workouts.Dtos;
using Health.Core.Resources;
using Health.DAL;
using Health.Domain.Models.Enums;
using Health.Domain.Models.Response;
using MediatR;

namespace Health.Core.Features.Workouts.Command.Delete;

public class DeleteWorkoutCommandHandler(ApplicationDbContext context, IMapper mapper)
    : IRequestHandler<DeleteWorkoutCommand, BaseResponse<WorkoutDto>>
{
    public async Task<BaseResponse<WorkoutDto>> Handle(DeleteWorkoutCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var workout = await context.Workouts.FindAsync([request.Id], cancellationToken);

            if (workout == null)
            {
                return new BaseResponse<WorkoutDto>
                {
                    ErrorCode = (int)ErrorCode.WorkoutNotFound,
                    ErrorMessage = ErrorMessages.WorkoutNotFound
                };
            }

            context.Workouts.Remove(workout);
            await context.SaveChangesAsync(cancellationToken);

            return new BaseResponse<WorkoutDto>
            {
                Data = mapper.Map<WorkoutDto>(workout),
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<WorkoutDto>
            {
                ErrorCode = (int)ErrorCode.InternalServerError,
                ErrorMessage = ErrorMessages.InternalServerError + " -> " + ex.Message
            };
        }
    }
}

