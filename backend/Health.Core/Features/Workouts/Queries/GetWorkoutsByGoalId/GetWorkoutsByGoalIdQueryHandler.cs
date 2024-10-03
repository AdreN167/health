using AutoMapper;
using AutoMapper.QueryableExtensions;
using Health.Core.Features.Exercises.Dtos;
using Health.Core.Features.Goals.Dtos;
using Health.Core.Features.Trainers.Dtos;
using Health.Core.Features.Workouts.Dtos;
using Health.Core.Resources;
using Health.DAL;
using Health.Domain.Models.Enums;
using Health.Domain.Models.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Health.Core.Features.Workouts.Queries.GetWorkoutsByGoalId;

public class GetWorkoutsByGoalIdQueryHandler(ApplicationDbContext context, IMapper mapper)
    : IRequestHandler<GetWorkoutsByGoalIdQuery, CollectionResponse<ExtendedWorkoutDto>>
{
    public async Task<CollectionResponse<ExtendedWorkoutDto>> Handle(GetWorkoutsByGoalIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var goal = await context.Goals.FindAsync([request.Id], cancellationToken);

            if (goal == null)
            {
                return new CollectionResponse<ExtendedWorkoutDto>
                {
                    ErrorCode = (int)ErrorCode.GoalNotFound,
                    ErrorMessage = ErrorMessages.GoalNotFound
                };
            }

            var workouts = await context.Workouts
                .Where(workout => workout.GoalId == request.Id)
                .ProjectTo<ExtendedWorkoutDto>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new CollectionResponse<ExtendedWorkoutDto>
            {
                Data = workouts,
                Count = workouts.Count
            };
        }
        catch (Exception ex)
        {
            return new CollectionResponse<ExtendedWorkoutDto>
            {
                ErrorCode = (int)ErrorCode.InternalServerError,
                ErrorMessage = ErrorMessages.InternalServerError + " -> " + ex.Message
            };
        }
    }
}

