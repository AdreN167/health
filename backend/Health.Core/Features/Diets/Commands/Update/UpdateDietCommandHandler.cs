using AutoMapper;
using Health.Core.Features.Diets.Dtos;
using Health.Core.Resources;
using Health.DAL;
using Health.Domain.Models.Enums;
using Health.Domain.Models.Response;
using MediatR;

namespace Health.Core.Features.Diets.Commands.Update;

public class UpdateDietCommandHandler(ApplicationDbContext context, IMapper mapper)
    : IRequestHandler<UpdateDietCommand, BaseResponse<DietDto>>
{
    public async Task<BaseResponse<DietDto>> Handle(UpdateDietCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return new BaseResponse<DietDto>
                {
                    ErrorCode = (int)ErrorCode.InvalidRequest,
                    ErrorMessage = ErrorMessages.InvalidRequest
                };
            }

            var diet = await context.Diets.FindAsync([request.Id], cancellationToken);

            if (diet == null)
            {
                return new BaseResponse<DietDto>
                {
                    ErrorCode = (int)ErrorCode.WorkoutNotFound,
                    ErrorMessage = ErrorMessages.WorkoutNotFound
                };
            }

            diet.Name = request.Name;

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

