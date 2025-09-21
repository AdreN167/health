using Health.Core.Features.Diets.Dtos;
using Health.Domain.Models.Response;
using MediatR;

namespace Health.Core.Features.Diets.Queries.GetDietsByGoalId;

public record GetDietsByGoalIdQuery(long Id) : IRequest<CollectionResponse<ExtendedDietDto>>;
