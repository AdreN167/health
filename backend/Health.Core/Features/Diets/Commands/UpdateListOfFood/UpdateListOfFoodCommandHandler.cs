using AutoMapper;
using Health.Core.Features.Diets.Dtos;
using Health.Core.Resources;
using Health.DAL;
using Health.Domain.Models.Entities;
using Health.Domain.Models.Enums;
using Health.Domain.Models.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Health.Core.Features.Diets.Commands.UpdateListOfFood;

public class UpdateListOfFoodCommandHandler(ApplicationDbContext context, IMapper mapper)
    : IRequestHandler<UpdateListOfFoodCommand, BaseResponse<ExtendedDietDto>>
{
    public async Task<BaseResponse<ExtendedDietDto>> Handle(UpdateListOfFoodCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var diet = await context.Diets.FindAsync([request.Id], cancellationToken);

            if (diet == null)
            {
                return new BaseResponse<ExtendedDietDto>
                {
                    ErrorCode = (int)ErrorCode.DietNotFound,
                    ErrorMessage = ErrorMessages.DietNotFound,
                };
            }

            // Продукты
            var productIds = request.ProductsWithWeight.Keys.Select(long.Parse);
            var products = await context.Products
                .Where(x => productIds.Contains(x.Id))
                .ToListAsync(cancellationToken);

            if (products.Count != request.ProductsWithWeight.Count)
            {
                return new BaseResponse<ExtendedDietDto>
                {
                    ErrorCode = (int)ErrorCode.ProductNotFound,
                    ErrorMessage = ErrorMessages.ProductNotFound,
                };
            }

            diet.Products.Clear();

            foreach (var product in products)
            {
                diet.DietProducts!.Add(new DietProduct
                {
                    Product = product,
                    Weight = request.ProductsWithWeight[product.Id.ToString()]
                });
            }

            // Блюда
            var dishIds = request.DishesWithWeight.Keys.Select(long.Parse);
            var dishes = await context.Dishes
                .Where(x => dishIds.Contains(x.Id))
                .ToListAsync(cancellationToken);

            if (dishes.Count != request.DishesWithWeight.Count)
            {
                return new BaseResponse<ExtendedDietDto>
                {
                    ErrorCode = (int)ErrorCode.DishNotFound,
                    ErrorMessage = ErrorMessages.DishNotFound,
                };
            }

            diet.Dishes.Clear();

            foreach (var dish in dishes)
            {
                diet.DietDishes!.Add(new DietDish
                {
                    Dish = dish,
                    Weight = request.DishesWithWeight[dish.Id.ToString()]
                });
            }

            await context.SaveChangesAsync(cancellationToken);

            return new BaseResponse<ExtendedDietDto>
            {
                Data = mapper.Map<ExtendedDietDto>(diet)
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<ExtendedDietDto>
            {
                ErrorCode = (int)ErrorCode.InternalServerError,
                ErrorMessage = ErrorMessages.InternalServerError + " -> " + ex.Message
            };
        }
    }
}

