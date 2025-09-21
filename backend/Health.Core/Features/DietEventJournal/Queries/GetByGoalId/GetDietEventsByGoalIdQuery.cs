using Health.Core.Features.DietEventJournal.Dtos;
using Health.Domain.Models.Response;
using MediatR;

namespace Health.Core.Features.DietEventJournal.Queries.GetByGoalId;

public record GetDietEventsByGoalIdQuery(string Email, long GoalId) : IRequest<CollectionResponse<DietEventDto>>;

