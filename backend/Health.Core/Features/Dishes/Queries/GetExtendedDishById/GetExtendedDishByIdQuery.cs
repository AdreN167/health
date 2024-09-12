using Health.Core.Features.Dishes.Dto;
using Health.Domain.Models.Response;
using MediatR;

namespace Health.Core.Features.Dishes.Queries.GetExtendedDishById;

public record GetExtendedDishByIdQuery(long Id) : IRequest<BaseResponse<ExtendedDishDto>>;

