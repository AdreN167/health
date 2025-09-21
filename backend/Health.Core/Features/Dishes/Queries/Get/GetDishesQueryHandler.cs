using AutoMapper;
using AutoMapper.QueryableExtensions;
using Health.Core.Features.Dishes.Dto;
using Health.Core.Resources;
using Health.DAL;
using Health.Domain.Models.Enums;
using Health.Domain.Models.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Health.Core.Features.Dishes.Queries.Get;

public class GetDishesQueryHandler(ApplicationDbContext context, IMapper mapper)
    : IRequestHandler<GetDishesQuery, CollectionResponse<ExtendedDishDto>>
{
    public async Task<CollectionResponse<ExtendedDishDto>> Handle(GetDishesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var dishes = await context.Dishes
                .AsNoTracking()
                .ProjectTo<ExtendedDishDto>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new CollectionResponse<ExtendedDishDto> 
            {
                Data = dishes,
                Count = dishes.Count
            };
        }
        catch (Exception ex)
        {
            return new CollectionResponse<ExtendedDishDto>
            {
                ErrorCode = (int)ErrorCode.InternalServerError,
                ErrorMessage = ErrorMessages.InternalServerError + " -> " + ex.Message
            };
        }
    }
}

