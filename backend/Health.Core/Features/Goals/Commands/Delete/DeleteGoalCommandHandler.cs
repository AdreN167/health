using AutoMapper;
using Health.Core.Features.Goals.Dtos;
using Health.Core.Resources;
using Health.DAL;
using Health.Domain.Models.Enums;
using Health.Domain.Models.Response;
using MediatR;

namespace Health.Core.Features.Goals.Commands.Delete;

public class DeleteGoalCommandHandler(ApplicationDbContext context, IMapper mapper)
    : IRequestHandler<DeleteGoalCommand, BaseResponse<GoalDto>>
{
    public async Task<BaseResponse<GoalDto>> Handle(DeleteGoalCommand request, CancellationToken cancellationToken)
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

            context.Goals.Remove(goal);
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

