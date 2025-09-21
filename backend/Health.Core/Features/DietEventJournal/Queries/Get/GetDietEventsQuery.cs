using Health.Core.Features.DietEventJournal.Dtos;
using Health.Domain.Models.Response;
using MediatR;

namespace Health.Core.Features.DietEventJournal.Queries.Get;

public record GetDietEventsQuery(string Email) : IRequest<CollectionResponse<DietEventDto>>;

