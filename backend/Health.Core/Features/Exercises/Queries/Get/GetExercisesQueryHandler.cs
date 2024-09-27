using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

using Health.Core.Features.Exercises.Dtos;
using Health.Core.Resources;
using Health.DAL;
using Health.Domain.Models.Enums;
using Health.Domain.Models.Response;

namespace Health.Core.Features.Exercises.Queries.Get;

public class GetExercisesQueryHandler(ApplicationDbContext context, IMapper mapper)
    : IRequestHandler<GetExercisesQuery, CollectionResponse<ExerciseDto>>
{
    public async Task<CollectionResponse<ExerciseDto>> Handle(GetExercisesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var exercises = await context.Exercises
                .AsNoTracking()
                .ProjectTo<ExerciseDto>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new CollectionResponse<ExerciseDto>
            {
                Data = exercises,
                Count = exercises.Count
            };
        }
        catch (Exception ex)
        {
            return new CollectionResponse<ExerciseDto>
            {
                ErrorCode = (int)ErrorCode.InternalServerError,
                ErrorMessage = ErrorMessages.InternalServerError + " -> " + ex.Message
            };
        }
    }
}

