using AutoMapper;
using AutoMapper.QueryableExtensions;
using Health.Core.Features.Diets.Dtos;
using Health.Core.Features.Dishes.Dto;
using Health.Core.Features.Products.Dto;
using Health.Core.Resources;
using Health.DAL;
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

            var products = await context.Products
                .Where(x => request.ProductIds.Contains(x.Id))
                .ToListAsync(cancellationToken);

            if (products.Count != request.ProductIds.Count)
            {
                return new BaseResponse<ExtendedDietDto>
                {
                    ErrorCode = (int)ErrorCode.ProductNotFound,
                    ErrorMessage = ErrorMessages.ProductNotFound,
                };
            }

            var dishes = await context.Dishes
                .Where(x => request.DishIds.Contains(x.Id))
                .ToListAsync(cancellationToken);

            if (dishes.Count != request.DishIds.Count)
            {
                return new BaseResponse<ExtendedDietDto>
                {
                    ErrorCode = (int)ErrorCode.DishNotFound,
                    ErrorMessage = ErrorMessages.DishNotFound,
                };
            }

            diet.Products.Clear();

            foreach (var product in products)
            {
                diet.Products.Add(product);
            }

            diet.Dishes.Clear();

            foreach (var dish in dishes)
            {
                diet.Dishes.Add(dish);
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

