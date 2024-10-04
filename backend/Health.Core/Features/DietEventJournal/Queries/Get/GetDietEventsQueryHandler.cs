using AutoMapper;
using AutoMapper.QueryableExtensions;
using Health.Core.Features.DietEventJournal.Dtos;
using Health.Core.Resources;
using Health.DAL;
using Health.Domain.Models.Enums;
using Health.Domain.Models.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Health.Core.Features.DietEventJournal.Queries.Get;

public class GetDietEventsQueryHandler(ApplicationDbContext context, IMapper mapper)
    : IRequestHandler<GetDietEventsQuery, CollectionResponse<DietEventDto>>
{
    public async Task<CollectionResponse<DietEventDto>> Handle(GetDietEventsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var events = await context.DietEvents
                .AsNoTracking()
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

