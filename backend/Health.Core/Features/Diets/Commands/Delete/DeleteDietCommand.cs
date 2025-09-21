using Health.Core.Features.Diets.Dtos;
using Health.Domain.Models.Response;
using MediatR;

namespace Health.Core.Features.Diets.Commands.Delete;

public record DeleteDietCommand(long Id) : IRequest<BaseResponse<DietDto>>;

