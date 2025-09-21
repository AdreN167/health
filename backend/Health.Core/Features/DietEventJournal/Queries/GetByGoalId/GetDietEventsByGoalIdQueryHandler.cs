using AutoMapper;
using AutoMapper.QueryableExtensions;
using Health.Core.Features.DietEventJournal.Dtos;
using Health.Core.Resources;
using Health.DAL;
using Health.Domain.Models.Enums;
using Health.Domain.Models.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Health.Core.Features.DietEventJournal.Queries.GetByGoalId;

public class GetDietEventsByGoalIdQueryHandler(ApplicationDbContext context, IMapper mapper)
    : IRequestHandler<GetDietEventsByGoalIdQuery, CollectionResponse<DietEventDto>>
{
    public async Task<CollectionResponse<DietEventDto>> Handle(GetDietEventsByGoalIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var events = await context.DietEvents
                .AsNoTracking()
                .Where(de => de.Diet.Goal.User.Email.Equals(request.Email) && de.Diet.GoalId == request.GoalId)
                .ProjectTo<DietEventDto>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new CollectionResponse<DietEventDto>
            {
                Data = events,
                Count = events.Count
            };
        }
        catch (Exception ex)
        {
            return new CollectionResponse<DietEventDto>
            {
                ErrorCode = (int)ErrorCode.InternalServerError,
                ErrorMessage = ErrorMessages.InternalServerError + " -> " + ex.Message
            };
        }
    }
}

