using AutoMapper;
using AutoMapper.QueryableExtensions;
using Health.Core.Features.Goals.Dtos;
using Health.Core.Resources;
using Health.DAL;
using Health.Domain.Models.Enums;
using Health.Domain.Models.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Health.Core.Features.Goals.Queries.Get;

public class GetGoalsQueryHandler(ApplicationDbContext context, IMapper mapper)
    : IRequestHandler<GetGoalsQuery, CollectionResponse<GoalDto>>
{
    public async Task<CollectionResponse<GoalDto>> Handle(GetGoalsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var goals = await context.Goals
                .AsNoTracking()
                .ProjectTo<GoalDto>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new CollectionResponse<GoalDto>
            {
                Data = goals,
                Count = goals.Count
            };
        }
        catch (Exception ex)
        {
            return new CollectionResponse<GoalDto>
            {
                ErrorCode = (int)ErrorCode.InternalServerError,
                ErrorMessage = ErrorMessages.InternalServerError + " -> " + ex.Message
            };
        }
    }
}

