using AutoMapper;
using AutoMapper.QueryableExtensions;
using Health.Core.Features.Diets.Dtos;
using Health.Core.Features.Workouts.Dtos;
using Health.Core.Resources;
using Health.DAL;
using Health.Domain.Models.Enums;
using Health.Domain.Models.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Health.Core.Features.Diets.Queries.GetDietsByGoalId;

public class GetDietsByGoalIdQueryHandler(ApplicationDbContext context, IMapper mapper)
    : IRequestHandler<GetDietsByGoalIdQuery, CollectionResponse<ExtendedDietDto>>
{
    public async Task<CollectionResponse<ExtendedDietDto>> Handle(GetDietsByGoalIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var goal = await context.Goals.FindAsync([request.Id], cancellationToken);

            if (goal == null)
            {
                return new CollectionResponse<ExtendedDietDto>
                {
                    ErrorCode = (int)ErrorCode.GoalNotFound,
                    ErrorMessage = ErrorMessages.GoalNotFound
                };
            }

            var diets = await context.Diets
                .Where(diet => diet.GoalId == request.Id)
                .ProjectTo<ExtendedDietDto>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new CollectionResponse<ExtendedDietDto>
            {
                Data = diets,
                Count = diets.Count
            };
        }
        catch (Exception ex)
        {
            return new CollectionResponse<ExtendedDietDto>
            {
                ErrorCode = (int)ErrorCode.InternalServerError,
                ErrorMessage = ErrorMessages.InternalServerError + " -> " + ex.Message
            };
        }
    }
}

