using AutoMapper;
using Health.Core.Features.Products.Dto;
using Health.Core.Resources;
using Health.DAL;
using Health.Domain.Models.Entities;
using Health.Domain.Models.Enums;
using Health.Domain.Models.Response;
using MediatR;

namespace Health.Core.Features.Products.Commands.Delete;

public class DeleteProductCommandHandler(ApplicationDbContext context, IMapper mapper)
    : IRequestHandler<DeleteProductCommand, BaseResponse<ProductDto>>
{
    public async Task<BaseResponse<ProductDto>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var product = await context.Products.FindAsync([request.Id], cancellationToken);

            if (product == null)
            {
                return new BaseResponse<ProductDto>
                {
                    ErrorCode = (int)ErrorCode.ProductNotFound,
                    ErrorMessage = ErrorMessages.ProductNotFound
                };
            }

            if (!string.IsNullOrWhiteSpace(product.FileName))
            {
                var folder = @"wwwroot\uploads\products";
                File.Delete(Path.Combine(folder, product.FileName));
            }

            context.Products.Remove(product);
            await context.SaveChangesAsync(cancellationToken);

            return new BaseResponse<ProductDto>
            {
                Data = mapper.Map<ProductDto>(product)
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<ProductDto>
            {
                ErrorCode = (int)ErrorCode.InternalServerError,
                ErrorMessage = ErrorMessages.InternalServerError + " -> " + ex.Message
            };
        }
    }
}

