using AutoMapper;
using Health.Core.Features.Workouts.Dtos;
using Health.Core.Resources;
using Health.DAL;
using Health.Domain.Models.Entities;
using Health.Domain.Models.Enums;
using Health.Domain.Models.Response;
using MediatR;

namespace Health.Core.Features.Workouts.Command.Update;

public class UpdateWorkoutCommandHandler(ApplicationDbContext context, IMapper mapper)
    : IRequestHandler<UpdateWorkoutCommand, BaseResponse<WorkoutDto>>
{
    public async Task<BaseResponse<WorkoutDto>> Handle(UpdateWorkoutCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return new BaseResponse<WorkoutDto>
                {
                    ErrorCode = (int)ErrorCode.InvalidRequest,
                    ErrorMessage = ErrorMessages.InvalidRequest
                };
            }

            var workout = await context.Workouts.FindAsync([request.Id], cancellationToken);

            if (workout == null)
            {
                return new BaseResponse<WorkoutDto>
                {
                    ErrorCode = (int)ErrorCode.WorkoutNotFound,
                    ErrorMessage = ErrorMessages.WorkoutNotFound
                };
            }

            workout.Name = request.Name;

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

