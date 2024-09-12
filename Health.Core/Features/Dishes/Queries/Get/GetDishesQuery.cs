using Health.Core.Features.Dishes.Dto;
using Health.Domain.Models.Response;
using MediatR;

namespace Health.Core.Features.Dishes.Queries.Get;

public record GetDishesQuery : IRequest<CollectionResponse<DishDto>>;

