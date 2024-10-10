using AutoMapper;
using Health.Core.Features.Dishes.Dto;
using Health.Core.Resources;
using Health.DAL;
using Health.Domain.Models.Entities;
using Health.Domain.Models.Enums;
using Health.Domain.Models.Response;
using MediatR;

namespace Health.Core.Features.Dishes.Commands.UpdateListOfProductsByDishId;

public class UpdateListOfProductsByDishIdCommandHandler(ApplicationDbContext context, IMapper mapper)
    : IRequestHandler<UpdateListOfProductsByDishIdCommand, BaseResponse<ExtendedDishDto>>
{
    public async Task<BaseResponse<ExtendedDishDto>> Handle(UpdateListOfProductsByDishIdCommand request, CancellationToken cancellationToken)
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

            if (request.ProductsWithWeight.Count == 0)
            {
                return new BaseResponse<ExtendedDishDto>
                {
                    ErrorCode = (int)ErrorCode.InvalidRequest,
                    ErrorMessage = ErrorMessages.InvalidRequest
                };
            }

            var products = context.Products
                .AsEnumerable()
                .Where(x => request.ProductsWithWeight.ContainsKey(x.Id.ToString()))
                .ToList();

            if (request.ProductsWithWeight.Count != products.Count)
            {
                return new BaseResponse<ExtendedDishDto>
                {
                    ErrorCode = (int)ErrorCode.ProductNotFound,
                    ErrorMessage = ErrorMessages.ProductNotFound
                };
            }

            dish.DishProducts?.Clear();
            dish.DishProducts = new List<DishProduct>();

            foreach (var product in products)
            {
                dish.DishProducts.Add(new DishProduct
                {
                    Product = product,
                    Weight = request.ProductsWithWeight[product.Id.ToString()]
                });
            }

            await context.SaveChangesAsync(cancellationToken);

            return new BaseResponse<ExtendedDishDto>
            {
                Data = mapper.Map<ExtendedDishDto>(dish),
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

