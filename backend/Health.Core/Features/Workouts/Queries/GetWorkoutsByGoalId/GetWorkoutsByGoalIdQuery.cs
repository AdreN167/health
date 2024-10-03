using Health.Core.Features.Workouts.Dtos;
using Health.Domain.Models.Response;
using MediatR;

namespace Health.Core.Features.Workouts.Queries.GetWorkoutsByGoalId;

public record GetWorkoutsByGoalIdQuery(long Id) : IRequest<CollectionResponse<ExtendedWorkoutDto>>;

