using AutoMapper;
using AutoMapper.QueryableExtensions;
using Health.Core.Features.Diets.Dtos;
using Health.Core.Features.Dishes.Dto;
using Health.Core.Resources;
using Health.DAL;
using Health.Domain.Models.Common;
using Health.Domain.Models.Entities;
using Health.Domain.Models.Enums;
using Health.Domain.Models.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.Arm;

namespace Health.Core.Features.DietEventJournal.Commands.Create;

public class CreateDietEventCommandHandler(ApplicationDbContext context, IMapper mapper)
    : IRequestHandler<CreateDietEventCommand, BaseResponse<long>>
{
    public async Task<BaseResponse<long>> Handle(CreateDietEventCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (!DateTime.TryParse(request.Date, out var date))
            {
                return new BaseResponse<long>
                {
                    ErrorCode = (int)ErrorCode.InvalidRequest,
                    ErrorMessage = ErrorMessages.InvalidRequest,
                };
            }

            var dietEntity = await context.Diets.FindAsync([request.DietId], cancellationToken);
            var diet = mapper.Map<ExtendedDietDto>(dietEntity);

            if (diet == null)
            {
                return new BaseResponse<long>
                {
                    ErrorCode = (int)ErrorCode.DietNotFound,
                    ErrorMessage = ErrorMessages.DietNotFound
                };
            }

            double calories = 0;
            double carbohydrates = 0;
            double proteins = 0;
            double fats = 0;

            if (request.ProductIds != null && request.ProductIds.Count != 0 && diet.Products != null)
            {
                var products = diet.Products
                    .Where(product => request.ProductIds.Contains(product.Id))
                    .ToList();

                if (products.Count != request.ProductIds.Count)
                {
                    return new BaseResponse<long>
                    {
                        ErrorCode = (int)ErrorCode.ProductNotFound,
                        ErrorMessage = ErrorMessages.ProductNotFound
                    };
                }

                calories += products.Sum(product => product.Calories);
                carbohydrates += products.Sum(product => product.Carbohydrates);
                proteins += products.Sum(product => product.Proteins);
                fats += products.Sum(product => product.Fats);
            }

            if (request.DishIds != null && request.DishIds.Count != 0 && diet.Dishes != null)
            {
                var dishes = diet.Dishes
                    .Where(dish => request.DishIds.Contains(dish.Id))
                    .ToList();

                if (dishes.Count != request.DishIds.Count)
                {
                    return new BaseResponse<long>
                    {
                        ErrorCode = (int)ErrorCode.DishNotFound,
                        ErrorMessage = ErrorMessages.DishNotFound
                    };
                }

                calories += dishes.Sum(dish => dish.Calories);
                carbohydrates += dishes.Sum(dish => dish.Carbohydrates);
                proteins += dishes.Sum(dish => dish.Proteins);
                fats += dishes.Sum(dish => dish.Fats);
            }

            var newEvent = new DietEvent
            {
                Date = date,
                Diet = dietEntity,
                Calories = calories,
                Proteins = proteins,
                Fats = fats,
                Carbohydrates = carbohydrates
            };

            await context.DietEvents.AddAsync(newEvent, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return new BaseResponse<long>
            {
                Data = newEvent.Id
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

