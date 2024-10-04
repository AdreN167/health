using AutoMapper;
using AutoMapper.QueryableExtensions;
using Health.Core.Features.Products.Dto;
using Health.Core.Features.Products.Queries.Get;
using Health.Core.Features.WorkoutEventJournal.Dtos;
using Health.Core.Resources;
using Health.DAL;
using Health.Domain.Models.Enums;
using Health.Domain.Models.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Health.Core.Features.WorkoutEventJournal.Queries.Get;

public class GetWorkoutEventsQueryHandler(ApplicationDbContext context, IMapper mapper)
    : IRequestHandler<GetWorkoutEventsQuery, CollectionResponse<WorkoutEventDto>>
{
    public async Task<CollectionResponse<WorkoutEventDto>> Handle(GetWorkoutEventsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var events = await context.WorkoutEvents
                .AsNoTracking()
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

