using AutoMapper;
using Health.Core.Features.Users.Dtos;
using Health.Core.Resources;
using Health.DAL;
using Health.Domain.Models.Enums;
using Health.Domain.Models.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Health.Core.Features.Users.Queries.GetUsersInfo;

public class GetUsersInfoQueryHandler(ApplicationDbContext context, IMapper mapper)
    : IRequestHandler<GetUsersInfoQuery, BaseResponse<UsersInfoDto>>
{
    public async Task<BaseResponse<UsersInfoDto>> Handle(GetUsersInfoQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await context.Users.FirstOrDefaultAsync(user => user.Email.Equals(request.Email), cancellationToken);

            if (user == null)
            {
                return new BaseResponse<UsersInfoDto>
                {
                    ErrorCode = (int)ErrorCode.UserNotFound,
                    ErrorMessage = ErrorMessages.UserNotFound
                };
            }

            return new BaseResponse<UsersInfoDto>
            {
                Data = mapper.Map<UsersInfoDto>(user)
            };
        }
        catch
        {
            return new BaseResponse<UsersInfoDto>
            {
                ErrorCode = (int)ErrorCode.InternalServerError,
                ErrorMessage = ErrorMessages.InternalServerError
            };
        }
    }
}

