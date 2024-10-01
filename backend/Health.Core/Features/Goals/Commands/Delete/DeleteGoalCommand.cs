using Health.Core.Features.Goals.Dtos;
using Health.Domain.Models.Response;
using MediatR;

namespace Health.Core.Features.Goals.Commands.Delete;

public record DeleteGoalCommand(long Id) : IRequest<BaseResponse<GoalDto>>;

