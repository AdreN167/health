using Health.Core.Resources;
using Health.DAL;
using Health.Domain.Models.Common;
using Health.Domain.Models.Entities;
using Health.Domain.Models.Enums;
using Health.Domain.Models.Response;
using MediatR;

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

            var newDish = new Dish
            {
                Name = request.Name,
                Description = request.Description,
                DishProducts = new List<DishProduct>()
            };

            if (request.Image != null)
            {
                var newFileName = $"dish-{Guid.NewGuid()}-{request.Image.FileName}";
                var filePath = Path.Combine(Constants.DISHES_FOLDER, newFileName);

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

            await context.Dishes.AddAsync(newDish, cancellationToken);
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

