using AutoMapper;
using AutoMapper.QueryableExtensions;
using Health.Core.Features.WorkoutEventJournal.Dtos;
using Health.Core.Resources;
using Health.DAL;
using Health.Domain.Models.Enums;
using Health.Domain.Models.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Health.Core.Features.WorkoutEventJournal.Queries.GetByGoalId;

public class GetWorkoutEventsByGoalIdQueryHandler(ApplicationDbContext context, IMapper mapper)
    : IRequestHandler<GetWorkoutEventsByGoalIdQuery, CollectionResponse<WorkoutEventDto>>
{
    public async Task<CollectionResponse<WorkoutEventDto>> Handle(GetWorkoutEventsByGoalIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var events = await context.WorkoutEvents
                .AsNoTracking()
                .Where(de => de.Workout.Goal.User.Email.Equals(request.Email) && de.Workout.GoalId == request.GoalId)
                .ProjectTo<WorkoutEventDto>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new CollectionResponse<WorkoutEventDto>
            {
                Data = events,
                Count = events.Count
            };
        }
        catch (Exception ex)
        {
            return new CollectionResponse<WorkoutEventDto>
            {
                ErrorCode = (int)ErrorCode.InternalServerError,
                ErrorMessage = ErrorMessages.InternalServerError + " -> " + ex.Message
            };
        }
    }
}

