using AutoMapper;
using AutoMapper.QueryableExtensions;
using Health.Core.Features.Products.Dto;
using Health.Core.Features.Trainers.Dtos;
using Health.Core.Resources;
using Health.DAL;
using Health.Domain.Models.Enums;
using Health.Domain.Models.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Health.Core.Features.Trainers.Queries.Get;

public class GetTrainersQueryHandler(ApplicationDbContext context, IMapper mapper)
    : IRequestHandler<GetTrainersQuery, CollectionResponse<TrainerDto>>
{
    public async Task<CollectionResponse<TrainerDto>> Handle(GetTrainersQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var trainers = await context.Trainers
                .AsNoTracking()
                .ProjectTo<TrainerDto>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new CollectionResponse<TrainerDto>
            {
                Data = trainers,
                Count = trainers.Count
            };
        }
        catch (Exception ex)
        {
            return new CollectionResponse<TrainerDto>
            {
                ErrorCode = (int)ErrorCode.InternalServerError,
                ErrorMessage = ErrorMessages.InternalServerError + " -> " + ex.Message
            };
        }
    }
}

