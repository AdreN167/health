using Health.Core.Features.Goals.Dtos;
using Health.Domain.Models.Response;
using MediatR;

namespace Health.Core.Features.Goals.Queries.GetGoalsByUserEmail;

public record GetGoalsByUserEmailQuery(string Email) : IRequest<CollectionResponse<GoalDto>>;

