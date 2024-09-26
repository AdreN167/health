using Health.Core.Features.Trainers.Dtos;
using Health.Domain.Models.Response;
using MediatR;

namespace Health.Core.Features.Trainers.Commands.Delete;

public record DeleteTrainerCommand(long Id) : IRequest<BaseResponse<TrainerDto>>;

