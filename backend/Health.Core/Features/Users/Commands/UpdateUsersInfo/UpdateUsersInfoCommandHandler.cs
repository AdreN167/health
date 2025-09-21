using AutoMapper;
using Health.Core.Features.Users.Dtos;
using Health.Core.Resources;
using Health.DAL;
using Health.Domain.Models.Enums;
using Health.Domain.Models.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Health.Core.Features.Users.Commands.UpdateUsersInfo;

public class UpdateUsersInfoCommandHandler(ApplicationDbContext context, IMapper mapper)
    : IRequestHandler<UpdateUsersInfoCommand, BaseResponse<UsersInfoDto>>
{
    public async Task<BaseResponse<UsersInfoDto>> Handle(UpdateUsersInfoCommand request, CancellationToken cancellationToken)
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

            if (request.Age <= 0 || request.Height <= 0 || request.Weight <= 0)
            {
                return new BaseResponse<UsersInfoDto>()
                {
                    ErrorMessage = ErrorMessages.InvalidRequest,
                    ErrorCode = (int)ErrorCode.InvalidRequest
                };
            }

            user.Age = request.Age;
            user.Height = request.Height;
            user.Weight = request.Weight;

            await context.SaveChangesAsync(cancellationToken);

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

