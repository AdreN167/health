using Health.Core.Features.WorkoutEventJournal.Dtos;
using Health.Domain.Models.Response;
using MediatR;

namespace Health.Core.Features.WorkoutEventJournal.Queries.Get;

public record GetWorkoutEventsQuery(string Email) : IRequest<CollectionResponse<WorkoutEventDto>>;

