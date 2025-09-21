using Health.Core.Features.Trainers.Dtos;
using Health.Domain.Models.Response;
using MediatR;

namespace Health.Core.Features.Trainers.Queries.Get;

public record GetTrainersQuery : IRequest<CollectionResponse<TrainerDto>>;

