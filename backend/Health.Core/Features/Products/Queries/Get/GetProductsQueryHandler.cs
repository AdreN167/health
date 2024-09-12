using AutoMapper;
using AutoMapper.QueryableExtensions;
using Health.Core.Features.Products.Dto;
using Health.Core.Resources;
using Health.DAL;
using Health.Domain.Models.Enums;
using Health.Domain.Models.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Health.Core.Features.Products.Queries.Get;

public class GetProductsQueryHandler(ApplicationDbContext context, IMapper mapper)
    : IRequestHandler<GetProductsQuery, CollectionResponse<ProductDto>>
{
    public async Task<CollectionResponse<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var products = await context.Products
                .AsQueryable()
                .ProjectTo<ProductDto>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new CollectionResponse<ProductDto>
            {
                Data = products,
                Count = products.Count
            };
        }
        catch (Exception ex)
        {
            return new CollectionResponse<ProductDto>
            {
                ErrorCode = (int)ErrorCode.InternalServerError,
                ErrorMessage = ErrorMessages.InternalServerError + " -> " + ex.Message
            };
        }
    }
}

