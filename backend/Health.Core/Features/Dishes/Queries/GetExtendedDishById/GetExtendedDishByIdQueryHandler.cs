using AutoMapper;
using MediatR;
using Health.Core.Features.Dishes.Dto;
using Health.Domain.Models.Response;
using Health.DAL;
using Health.Core.Resources;
using Health.Domain.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace Health.Core.Features.Dishes.Queries.GetExtendedDishById;

public class GetExtendedDishByIdQueryHandler(ApplicationDbContext context, IMapper mapper)
    : IRequestHandler<GetExtendedDishByIdQuery, BaseResponse<ExtendedDishDto>>
{
    public async Task<BaseResponse<ExtendedDishDto>> Handle(GetExtendedDishByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var dish = await context.Dishes.FindAsync([request.Id], cancellationToken);

            if (dish == null)
            {
                return new BaseResponse<ExtendedDishDto>
                {
                    ErrorCode = (int)ErrorCode.DishNotFound,
                    ErrorMessage = ErrorMessages.DishNotFound
                };
            }

            return new BaseResponse<ExtendedDishDto>
            {
                Data = mapper.Map<ExtendedDishDto>(dish)
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<ExtendedDishDto>
            {
                ErrorCode = (int)ErrorCode.InternalServerError,
                ErrorMessage = ErrorMessages.InternalServerError + " -> " + ex.Message
            };
        }
    }
}

