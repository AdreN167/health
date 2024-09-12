using Health.Core.Features.Dishes.Dto;
using Health.Domain.Models.Response;
using MediatR;

namespace Health.Core.Features.Dishes.Commands.Delete;

public record DeleteDishCommand(long Id) : IRequest<BaseResponse<DishDto>>;

