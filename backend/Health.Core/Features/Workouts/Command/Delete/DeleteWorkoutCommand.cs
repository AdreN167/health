using Health.Core.Features.Workouts.Dtos;
using Health.Domain.Models.Response;
using MediatR;

namespace Health.Core.Features.Workouts.Command.Delete;

public record DeleteWorkoutCommand(long Id) : IRequest<BaseResponse<WorkoutDto>>;
