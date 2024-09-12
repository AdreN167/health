using Health.Core.Resources;
using Health.DAL;
using Health.Domain.Models.Entities;
using Health.Domain.Models.Enums;
using Health.Domain.Models.Response;
using MediatR;

namespace Health.Core.Features.Products.Commands.Create;

public class CreateProductCommandHandler(ApplicationDbContext context)
    : IRequestHandler<CreateProductCommand, BaseResponse<long>>
{
    public async Task<BaseResponse<long>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Name)
            || request.Calories <= 0
            || request.Proteins <= 0
            || request.Fats <= 0
            || request.Carbohydrates <= 0)
        {
            return new BaseResponse<long>
            {
                ErrorCode = (int)ErrorCode.InvalidRequest,
                ErrorMessage = ErrorMessages.InvalidRequest,
            };
        }

        Product newProduct = new Product
        {
            Name = request.Name,
            Proteins = request.Proteins,
            Calories = request.Calories,
            Carbohydrates = request.Carbohydrates,
            Fats = request.Fats,
            Dishes = new List<Dish>()
        };

        try
        {
            await context.Products.AddAsync(newProduct);
            await context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            return new BaseResponse<long>
            {
                ErrorCode = (int)ErrorCode.InternalServerError,
                ErrorMessage = ErrorMessages.InternalServerError + " -> " + ex.Message
            };
        }

        return new BaseResponse<long>
        {
            Data = newProduct.Id
        };
    }
}

