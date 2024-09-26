using AutoMapper;
using Health.Core.Features.Dishes.Dto;
using Health.Core.Resources;
using Health.DAL;
using Health.Domain.Models.Common;
using Health.Domain.Models.Enums;
using Health.Domain.Models.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Health.Core.Features.Dishes.Commands.Update;

public class UpdateDishCommandHandler(ApplicationDbContext context, IMapper mapper)
    : IRequestHandler<UpdateDishCommand, BaseResponse<DishDto>>
{
    public async Task<BaseResponse<DishDto>> Handle(UpdateDishCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var dish = await context.Dishes.FindAsync([request.Id], cancellationToken);

            if (dish == null)
            {
                return new BaseResponse<DishDto>
                {
                    ErrorCode = (int)ErrorCode.DishNotFound,
                    ErrorMessage = ErrorMessages.DishNotFound
                };
            }

            if (string.IsNullOrWhiteSpace(request.Description) 
                || string.IsNullOrWhiteSpace(request.Name)
                || request.ProductIds.Count == 0)
            {
                return new BaseResponse<DishDto>
                {
                    ErrorCode = (int)ErrorCode.InvalidRequest,
                    ErrorMessage = ErrorMessages.InvalidRequest
                };
            }

            await context.Entry(dish).Collection(e => e.Products).LoadAsync(cancellationToken); // подгружаем продукты в блюдо

            var productsToAdd = await context.Products
                .Where(x => request.ProductIds.Contains(x.Id))
                .ToListAsync(cancellationToken);

            if (request.ProductIds.Count != productsToAdd.Count)
            {
                return new BaseResponse<DishDto>
                {
                    ErrorCode = (int)ErrorCode.ProductNotFound,
                    ErrorMessage = ErrorMessages.ProductNotFound
                };
            }

            dish.Name = request.Name;
            dish.Description = request.Description;
            dish.Products.Clear();

            foreach (var product in productsToAdd)
            {
                dish.Products!.Add(product);
            }

            if (request.Image != null)
            {
                var folder = Constants.DISHES_FOLDER;

                if (!string.IsNullOrWhiteSpace(dish.FileName))
                {
                    File.Delete(Path.Combine(folder, dish.FileName));
                }

                var newFileName = $"dish-{Guid.NewGuid()}-{request.Image.FileName}";
                var filePath = Path.Combine(folder, newFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await request.Image.CopyToAsync(stream);
                }

                dish.FileName = newFileName;
            }

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

