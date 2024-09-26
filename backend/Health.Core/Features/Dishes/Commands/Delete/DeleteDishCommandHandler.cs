using AutoMapper;
using Health.Core.Features.Dishes.Dto;
using Health.Core.Resources;
using Health.DAL;
using Health.Domain.Models.Common;
using Health.Domain.Models.Enums;
using Health.Domain.Models.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Health.Core.Features.Dishes.Commands.Delete;

public class DeleteDishCommandHandler(ApplicationDbContext context, IMapper mapper)
    : IRequestHandler<DeleteDishCommand, BaseResponse<DishDto>>
{
    public async Task<BaseResponse<DishDto>> Handle(DeleteDishCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var dish = await context.Dishes.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (dish == null)
            {
                return new BaseResponse<DishDto>
                {
                    ErrorCode = (int)ErrorCode.DishNotFound,
                    ErrorMessage = ErrorMessages.DishNotFound
                };
            }

            if (!string.IsNullOrWhiteSpace(dish.FileName))
            {
                File.Delete(Path.Combine(Constants.DISHES_FOLDER, dish.FileName));
            }

            context.Remove(dish);
            await context.SaveChangesAsync(cancellationToken);

            return new BaseResponse<DishDto>
            {
                Data = mapper.Map<DishDto>(dish)
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<DishDto>
            {
                ErrorCode = (int)ErrorCode.InternalServerError,
                ErrorMessage = ErrorMessages.InternalServerError + " -> " + ex.Message
            };
        }
    }
}

