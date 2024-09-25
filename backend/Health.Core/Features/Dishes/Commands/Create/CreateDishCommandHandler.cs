using Health.Core.Resources;
using Health.DAL;
using Health.Domain.Models.Entities;
using Health.Domain.Models.Enums;
using Health.Domain.Models.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Health.Core.Features.Dishes.Commands.Create;

public class CreateDishCommandHandler(ApplicationDbContext context)
    : IRequestHandler<CreateDishCommand, BaseResponse<long>>
{
    public async Task<BaseResponse<long>> Handle(CreateDishCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.Description) || string.IsNullOrWhiteSpace(request.Name))
            {
                return new BaseResponse<long>
                {
                    ErrorCode = (int)ErrorCode.InvalidRequest,
                    ErrorMessage = ErrorMessages.InvalidRequest
                };
            }

            List<Product> products = await context.Products
                .Where(x => request.ProductIds.Contains(x.Id))
                .ToListAsync(cancellationToken);

            if (request.ProductIds.Count != products.Count)
            {
                return new BaseResponse<long>
                {
                    ErrorCode = (int)ErrorCode.ProductNotFound,
                    ErrorMessage = ErrorMessages.ProductNotFound
                };
            }

            var newDish = new Dish
            {
                Name = request.Name,
                Description = request.Description,
                Products = products
            };

            if (request.Image != null)
            {
                var newFileName = $"dish-{request.Image.FileName}";
                var filePath = Path.Combine(@"wwwroot\uploads\dishes", newFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await request.Image.CopyToAsync(stream);
                }

                newDish.FileName = newFileName;
            }
            else
            {
                newDish.FileName = string.Empty;
            }

            await context.Dishes.AddAsync(newDish);
            await context.SaveChangesAsync(cancellationToken);

            return new BaseResponse<long>
            {
                Data = newDish.Id
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

