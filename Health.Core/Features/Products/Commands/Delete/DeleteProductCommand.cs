using Health.Core.Features.Products.Dto;
using Health.Domain.Models.Response;
using MediatR;

namespace Health.Core.Features.Products.Commands.Delete;

public record DeleteProductCommand(long Id) : IRequest<BaseResponse<ProductDto>>;

