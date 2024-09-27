using AutoMapper;
using Health.Core.Features.Goals.Dtos;
using Health.Core.Resources;
using Health.DAL;
using Health.Domain.Models.Enums;
using Health.Domain.Models.Response;
using MediatR;
using System.Globalization;

namespace Health.Core.Features.Goals.Commands.Update;

public class UpdateGoalCommandHandler(ApplicationDbContext context, IMapper mapper)
    : IRequestHandler<UpdateGoalCommand, BaseResponse<GoalDto>>
{
    public async Task<BaseResponse<GoalDto>> Handle(UpdateGoalCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var goal = await context.Goals.FindAsync([request.Id], cancellationToken);

            if (goal == null)
            {
                return new BaseResponse<GoalDto>
                {
                    ErrorCode = (int)ErrorCode.GoalNotFound,
                    ErrorMessage = ErrorMessages.GoalNotFound
                };
            }

            if (string.IsNullOrWhiteSpace(request.Name) 
                || string.IsNullOrWhiteSpace(request.Deadline)
                || !DateTime.TryParse(request.Deadline, out var deadline))
            {
                return new BaseResponse<GoalDto>
                {
                    ErrorCode = (int)ErrorCode.InvalidRequest,
                    ErrorMessage = ErrorMessages.InvalidRequest
                };
            }

            goal.Name = request.Name;
            goal.Description = request.Description ?? "";
            goal.Deadline = deadline;

            await context.SaveChangesAsync(cancellationToken);

            return new BaseResponse<GoalDto>
            {
                Data = mapper.Map<GoalDto>(goal)
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<GoalDto>
            {
                ErrorCode = (int)ErrorCode.InternalServerError,
                ErrorMessage = ErrorMessages.InternalServerError + " -> " + ex.Message
            };
        }
    }
}

