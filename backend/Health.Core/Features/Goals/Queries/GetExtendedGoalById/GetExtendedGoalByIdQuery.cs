using Health.Core.Features.Goals.Dtos;
using Health.Domain.Models.Response;
using MediatR;

namespace Health.Core.Features.Goals.Queries.GetExtendedGoalById;

public record GetExtendedGoalByIdQuery(long Id) : IRequest<BaseResponse<ExtendedGoalDto>>;

