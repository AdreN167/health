using Health.Core.Features.WorkoutEventJournal.Dtos;
using Health.Domain.Models.Response;
using MediatR;

namespace Health.Core.Features.WorkoutEventJournal.Queries.GetByGoalId;

public record GetWorkoutEventsByGoalIdQuery(string Email, long GoalId) : IRequest<CollectionResponse<WorkoutEventDto>>;

