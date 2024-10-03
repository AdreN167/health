using Health.Core.Resources;
using Health.DAL;
using Health.Domain.Models.Entities;
using Health.Domain.Models.Enums;
using Health.Domain.Models.Response;
using MediatR;

namespace Health.Core.Features.Diets.Commands.Create;

public class CreateDietCommandHandler(ApplicationDbContext context)
    : IRequestHandler<CreateDietCommand, BaseResponse<long>>
{
    public async Task<BaseResponse<long>> Handle(CreateDietCommand request, CancellationToken cancellationToken)
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

            var newDiet = new Diet
            {
                Name = request.Name,
                Goal = goal
            };

            await context.Diets.AddAsync(newDiet, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return new BaseResponse<long>
            {
                Data = newDiet.Id
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

