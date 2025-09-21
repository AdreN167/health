using AutoMapper;
using AutoMapper.QueryableExtensions;
using Health.Core.Features.Goals.Dtos;
using Health.Core.Resources;
using Health.DAL;
using Health.Domain.Models.Enums;
using Health.Domain.Models.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Health.Core.Features.Goals.Queries.GetGoalsByUserEmail;

public class GetGoalsByUserEmailQueryHandler(ApplicationDbContext context, IMapper mapper)
    : IRequestHandler<GetGoalsByUserEmailQuery, CollectionResponse<GoalDto>>
{
    public async Task<CollectionResponse<GoalDto>> Handle(GetGoalsByUserEmailQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await context.Users.FirstOrDefaultAsync(user => user.Email.Equals(request.Email), cancellationToken: cancellationToken);

            if (user == null)
            {
                return new CollectionResponse<GoalDto>
                {
                    ErrorCode = (int)ErrorCode.UserNotFound,
                    ErrorMessage = ErrorMessages.UserNotFound,
                };
            }

            var goals = await context.Goals
                .AsNoTracking()
                .Where(goal => goal.User.Email.Equals(request.Email))
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

