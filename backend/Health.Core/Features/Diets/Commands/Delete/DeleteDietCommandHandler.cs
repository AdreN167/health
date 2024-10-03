using AutoMapper;
using Health.Core.Features.Diets.Dtos;
using Health.Core.Features.Workouts.Dtos;
using Health.Core.Resources;
using Health.DAL;
using Health.Domain.Models.Enums;
using Health.Domain.Models.Response;
using MediatR;

namespace Health.Core.Features.Diets.Commands.Delete;

public class DeleteDietCommandHandler(ApplicationDbContext context, IMapper mapper)
    : IRequestHandler<DeleteDietCommand, BaseResponse<DietDto>>
{
    public async Task<BaseResponse<DietDto>> Handle(DeleteDietCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var diet = await context.Diets.FindAsync([request.Id], cancellationToken);

            if (diet == null)
            {
                return new BaseResponse<DietDto>
                {
                    ErrorCode = (int)ErrorCode.WorkoutNotFound,
                    ErrorMessage = ErrorMessages.WorkoutNotFound
                };
            }

            context.Diets.Remove(diet);
            await context.SaveChangesAsync(cancellationToken);

            return new BaseResponse<DietDto>
            {
                Data = mapper.Map<DietDto>(diet),
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<DietDto>
            {
                ErrorCode = (int)ErrorCode.InternalServerError,
                ErrorMessage = ErrorMessages.InternalServerError + " -> " + ex.Message
            };
        }
    }
}

