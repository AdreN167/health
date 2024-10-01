using Health.Core.Features.Goals.Dtos;
using Health.Domain.Models.Response;
using MediatR;

namespace Health.Core.Features.Goals.Queries.Get;

public record GetGoalsQuery : IRequest<CollectionResponse<GoalDto>>;

