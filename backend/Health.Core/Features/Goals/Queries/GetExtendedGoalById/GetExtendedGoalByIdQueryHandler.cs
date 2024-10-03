using AutoMapper;
using Health.Core.Features.Goals.Dtos;
using Health.Core.Resources;
using Health.DAL;
using Health.Domain.Models.Enums;
using Health.Domain.Models.Response;
using MediatR;

namespace Health.Core.Features.Goals.Queries.GetExtendedGoalById;

public class GetExtendedGoalByIdQueryHandler(ApplicationDbContext context, IMapper mapper)
    : IRequestHandler<GetExtendedGoalByIdQuery, BaseResponse<ExtendedGoalDto>>
{
    public async Task<BaseResponse<ExtendedGoalDto>> Handle(GetExtendedGoalByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var goal = await context.Goals.FindAsync([request.Id], cancellationToken);

            if (goal == null)
            {
                return new BaseResponse<ExtendedGoalDto>
                {
                    ErrorCode = (int)ErrorCode.GoalNotFound,
                    ErrorMessage = ErrorMessages.GoalNotFound
                };
            }

            context.Entry(goal).Collection(goal => goal.Workouts).Load();
            //context.Entry(goal).Collection(goal => goal.Diets).Load();

            return new BaseResponse<ExtendedGoalDto>
            {
                Data = mapper.Map<ExtendedGoalDto>(goal)
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<ExtendedGoalDto>
            {
                ErrorCode = (int)ErrorCode.InternalServerError,
                ErrorMessage = ErrorMessages.InternalServerError + " -> " + ex.Message
            };
        }
    }
}

