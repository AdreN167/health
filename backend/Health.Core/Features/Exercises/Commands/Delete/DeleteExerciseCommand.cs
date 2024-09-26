using Health.Core.Features.Exercises.Dtos;
using Health.Domain.Models.Response;
using MediatR;

namespace Health.Core.Features.Exercises.Commands.Delete;

public record DeleteExerciseCommand(long Id) : IRequest<BaseResponse<ExerciseDto>>;
