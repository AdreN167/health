using Health.Core.Features.Exercises.Dtos;
using Health.Domain.Models.Response;
using MediatR;

namespace Health.Core.Features.Exercises.Queries.Get;

public record GetExercisesQuery : IRequest<CollectionResponse<ExerciseDto>>;

