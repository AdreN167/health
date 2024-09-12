using Health.Core.Features.Products.Dto;
using Health.Domain.Models.Response;
using MediatR;

namespace Health.Core.Features.Products.Queries.Get;

public record GetProductsQuery : IRequest<CollectionResponse<ProductDto>>;

